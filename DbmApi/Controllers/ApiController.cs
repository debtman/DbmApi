using Dapper;
using DbmApi.Models;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace DbmApi.Controllers
{
    [ApiController]
    [Route("[controller]")] 
    public class ApiController : Controller
    {
        private readonly ILogger<ApiController> _logger;
        //private string connectionString;
        private FbConnection Fbcon;

        public ApiController(ILogger<ApiController> logger, FbConnection _conn)
        {
            _logger = logger;
            //connectionString = conn;
            Fbcon = _conn;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUserListAsync()
        {            
            var ulist = new List<UserService>(); ;
            using (IDbConnection db = Fbcon)
            {
                ulist = db.Query<UserService>($@"select userid as Id, usersysname as Usersysname, username as Username, shortname as Shortname from users").ToList();
            }
            
            var result = JsonConvert.SerializeObject(ulist);
            return Ok(result);
        }

       

    }
}
