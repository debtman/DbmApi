using System;
using FB = FirebirdSql.Data.FirebirdClient;

namespace DbmApi.Models
{
    public static class ConnectionParams
    {
        public static string DbName { get; set; }
        public static string ServerAddress { get; set; }
        public static int Port { get; set; }
        public static string ?Charset { get; set; }

        public static void LoadConfiguration(IConfiguration configuration)
        {
            DbName = configuration["ConnectionParams:DbName"] ?? "";
            ServerAddress = configuration["ConnectionParams:ServerAddress"] ?? "localhost";
            Port = int.TryParse(configuration["ConnectionParams:Port"], out var port) ? port : 3050;
            Charset = configuration["ConnectionParams:Charset"] ?? "UTF8";
        }

        public static string GetConnectionString(string user, string password, string role)
        {            
            FB.FbConnectionStringBuilder connectString = new FB.FbConnectionStringBuilder();            
            connectString.Database = DbName;
            connectString.UserID = user;
            connectString.Password = password;
            connectString.DataSource = ServerAddress;
            connectString.Port = Port;
            if (!string.IsNullOrEmpty(role)) { connectString.Role = role; }
            connectString.Pooling = true;
            connectString.MinPoolSize = 5;
            connectString.MaxPoolSize = 10;
            connectString.Charset = Charset;
            return connectString.ConnectionString;

        }
    }
}
