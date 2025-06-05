using DbmApi.Models;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Xml;
using static DbmApi.FbData.CommonData;

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
                
                
        public async Task<CommonData.DbmDataTable> GetDataAsync(string sql)
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

        public async Task<CommonData.DbmDataSet> GetDataSetAsync(List<Tuple<string, string>> aQuery)
        {
            string errMessage = string.Empty;
            CommonData.DbmDataSet res = new CommonData.DbmDataSet();
            int i = 0;
            try
            { 
                foreach (var s in aQuery)
                {

                    DbmDataTable dt = await GetDataAsync(s.Item1);
                    if (string.IsNullOrEmpty(dt.message))
                    {
                        res.dmDataSet.Tables.Add(dt.dmtable);
                        res.dmDataSet.Tables[i].TableName = s.Item2;
                    }
                    else
                    {
                        errMessage += $"Ошибка чтения таблицы {s}: {dt.message}\n";
                    } 
                    i++;
                }
            }
            catch (Exception err)
            {
                res.message = $"Ошибка выполнения запроса: {err.Message}";
            }
            return res;
        }
                        

        public CommonData.DbmDataSet GetDataSetA(List<Tuple<string, string>> aQuery)
        {
            var t = Task.Run(async () => await GetDataSetAsync(aQuery));
            t.Wait();
            return t.Result;
        }
    }
}
