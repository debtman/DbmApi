using DbmApi.FbData;

namespace DbmApi.Models
{
    public class UserService
    {
        
        /*public User? Authenticate(string login, string password)
        {
            return _users.FirstOrDefault(u => u.Login == login);
        }*/
                

        public DbmUser? Authenticate(LoginRequest loginRequest)
        {
            DbmDataMgr dm = new DbmDataMgr();
            return dm.GetAuthInfo(loginRequest.Login, loginRequest.Password);
        }
        
    }
}
