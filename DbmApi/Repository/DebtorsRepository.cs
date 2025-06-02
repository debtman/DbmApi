using FirebirdSql.Data.FirebirdClient;
using Dapper;

namespace DbmApi.Repository
{
    public class DebtorsRepository
    {
        private IConfiguration _config;
        private string _constring { get { return _config.GetConnectionString("DbName"); } }

        public DebtorsRepository(IConfiguration config)
        {
            _config = config;
        }
    }
}
