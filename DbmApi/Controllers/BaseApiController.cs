using DbmApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

[ApiController]
public abstract class BaseApiController : ControllerBase
{
    protected readonly ILogger<BaseApiController> _logger;
    protected LoginRequest? LoginRequest { get; private set; }

    public BaseApiController(ILogger<BaseApiController> logger)
    {
        _logger = logger;
    }

    protected void ReceiveEncryptedCredentials()
    {
        if (Request.Headers.TryGetValue("Authorization", out var authHeader) &&
            Request.Headers.TryGetValue("X-Credentials", out var encryptedData))
        {
            string token = authHeader.ToString().Replace("Bearer ", "");
            string decryptedCredentials = DbmApi.Common.EncryptionHelper.DecryptData(encryptedData, token);

            LoginRequest = JsonSerializer.Deserialize<LoginRequest>(decryptedCredentials);
        }
    }

    protected IActionResult HandleException(Exception ex)
    {
        _logger.LogError(ex, "An error occurred in API.");
        return StatusCode(500, new { message = "Internal Server Error", error = ex.Message });
    }
}
