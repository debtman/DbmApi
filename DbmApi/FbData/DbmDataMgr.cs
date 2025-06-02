using DbmApi.Models;
using Microsoft.Extensions.FileSystemGlobbing.Internal;

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
                var result = fbd.GetData(sql).Result;
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
                        var testResult = fbw.GetData(sql).Result;
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
    }
}
