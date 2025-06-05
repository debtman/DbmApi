using DbmApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;
using System.Text.Json;

namespace DbmApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CryptController : Controller
    {
        // GET: api/Crypt
        [HttpGet]
        public ActionResult<string> Index()
        {
            return Ok(new { message = "CryptController Index" });
        }

        // GET: api/Crypt/Details/5
        [HttpGet("{p_str}/{key}")]
        public ActionResult<string> Crypt(string p_str, string key)
        {
            string encryptedData = Common.EncryptionHelper.EncryptData(p_str, key);
            return Ok(new { message = $"encrypted: {encryptedData}" });
        }

        [HttpGet("JSonTest")]
        public ActionResult<string> JsonTest()        
        {
            LoginRequest loginRequest = new LoginRequest
            {
                Login = "АДМИН",
                SystemName = "SYSDBA",
                Password = "masterkey",
                SystemRole = "UVD_HEAD"
            };
            string testStr = JsonSerializer.Serialize(loginRequest); 
            return Ok(testStr);
        }

        [HttpPost("Decrypt")]
        public ActionResult<string> Decrypt()
        {
            if (Request.Headers.TryGetValue("X-EncryptedData", out var encryptedData) &&
                Request.Headers.TryGetValue("X-Key", out var key))
            {
                try
                {
                    string decryptedData = Common.EncryptionHelper.DecryptData(encryptedData, key);
                    LoginRequest loginRequest = JsonSerializer.Deserialize<LoginRequest>(decryptedData);
                    return Ok(new { message = $"decrypted: {loginRequest.Login}" });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = "Decryption failed", error = ex.Message });
                }
            }
            return BadRequest(new { message = "Missing headers" });
        }

        // GET: api/Crypt/Create
        [HttpGet("create")]
        public ActionResult<string> Create()
        {
            return Ok(new { message = "Create endpoint accessed" });
        }

    }
}
