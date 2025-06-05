using DbmApi.FbData;
using DbmApi.Models;
using DbmShared.Dicts;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using static DbmApi.FbData.CommonData;

namespace DbmApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DictsController : BaseApiController
    {
        protected readonly ILogger<BaseApiController> _logger;
        
        public DictsController(ILogger<BaseApiController> logger) : base(logger)
        {
            _logger = logger;            
        }

        [HttpGet]
        public IActionResult GetDicts()
        {
            ReceiveEncryptedCredentials();
            DbmDataMgr dm = new DbmDataMgr();
            DictsModel dicts = new DictsModel(true);
            DbmDataSet ds = dm.GetDicts(LoginRequest,dicts.DataInfo);
            if (ds.dmDataSet.Tables.Count == 0)
            {
                ds.message = "No data found for the given credentials.";
            }

            MetaDict metaDict = new MetaDict();
            metaDict = dicts.FillDicts(ds);
            string jsonResponse = JsonSerializer.Serialize(metaDict, new JsonSerializerOptions { WriteIndented = true });

            return Ok(jsonResponse);
        }
        
    }
}
