using DbmApi.Models;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using static DbmApi.FbData.CommonData;

namespace DbmApi.FbData
{
    public class DbmDataMgr
    {
        private string WorkConString { get; set; } = string.Empty;
        private string AuthConString { get; set; } = string.Empty;
        public DbmDataMgr()
        {
            AuthConString = ConnectionParams.GetConnectionString("SYSTEMUSER", "systemuser", string.Empty);
        }

        public DbmUser GetAuthInfo(string _user, string _password)
        {
            //1. Получаем инфу о пользователе из базы данных
            FBDriver fbd = new FBDriver(AuthConString);
            DbmUser retVal = null;
            if (fbd != null)
            {
                var sql = $"SELECT * FROM USERS WHERE shortName = '{_user.ToUpper()}'";
                var result = fbd.GetDataAsync(sql).Result;
                if (result.dmtable != null && string.IsNullOrEmpty(result.message))
                {
                    if (result.dmtable.Rows.Count > 0)
                    {
                        var row = result.dmtable.Rows[0];
                        retVal = new DbmUser
                        {
                            Login = row["shortname"].ToString() ?? string.Empty,
                            UserSysname = row["usersysname"].ToString() ?? string.Empty,
                            Name = row["username"].ToString() ?? string.Empty,
                            Password = _password,
                            SystemRole = row["usergroup"].ToString() ?? string.Empty,
                        };

                        // Проверка пароля
                        WorkConString = ConnectionParams.GetConnectionString(retVal.UserSysname, retVal.Password, retVal.SystemRole);
                        FBDriver fbw = new FBDriver(WorkConString);
                        var testSql = $"SELECT * FROM USERS WHERE UsershortName = '{_user}'";
                        var testResult = fbw.GetDataAsync(sql).Result;
                        if (testResult.dmtable == null)
                        {
                            throw new Exception($"Ошибка получения данных пользователя {retVal.Login} - неверный пароль");
                        }

                    }
                }
                else
                {
                    throw new Exception($"Ошибка получения данных пользователя: {result.message}");
                }

            }
            else
            {
                throw new Exception("Авторизация. Ошибка подключения к базе данных");
            }

            return retVal;

        }

        public DbmDataSet GetDicts(LoginRequest loginRequest, List<Tuple<string,string>> dictList)
        {
            DbmDataSet ds = new DbmDataSet();
            try
            {
                if (string.IsNullOrEmpty(WorkConString))
                {
                    WorkConString = ConnectionParams.GetConnectionString(loginRequest.SystemName, loginRequest.Password, loginRequest.SystemRole);
                }

                // Выполняем запрос к базе данных
                FBDriver fbd = new FBDriver(WorkConString);
                ds = fbd.GetDataSetA(dictList);
                if (ds.dmDataSet.Tables.Count == 0)
                {
                    ds.message = "Нет данных для отображения";
                }                
            }
            catch (Exception ex)
            {
                ds.message = $"Ошибка: {ex.Message}";
            }

            return ds;
        }
    }
}
