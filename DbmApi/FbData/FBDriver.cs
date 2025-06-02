using FirebirdSql.Data.FirebirdClient;
using System;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace DbmApi.FbData
{
    public class FBDriver 
    {
        private readonly string _connectionString;

        public FBDriver(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<string> ExecuteQueryAsync(string sql)
        {
            try
            {
                using var connection = new FbConnection(_connectionString);
                await connection.OpenAsync();
                using var command = new FbCommand(sql, connection);

                await command.ExecuteNonQueryAsync();
                return $"Запрос выполнен успешно";
            }
            catch (Exception ex)
            {
                return $"Ошибка выполнения запроса: {ex.Message}";
            }
        }
                
                
        public async Task<CommonData.DbmDataTable> GetData(string sql)
        {
            var result = new CommonData.DbmDataTable { message = "" };
            try
            {
                using var connection = new FbConnection(_connectionString);
                connection.Open();

                using var command = new FbCommand(sql, connection);                
                command.CommandTimeout = 0;                
                command.CommandText = sql;
                FbDataReader reader = (FbDataReader)await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    result.dmtable.Load(reader);
                }
                else
                {
                    result.message = "Нет данных для отображения";
                }
            }
            catch(Exception err)
            {
                result.message = $"Ошибка выполнения запроса: {err.Message}";
            }
            return result;
        }
    }
}
