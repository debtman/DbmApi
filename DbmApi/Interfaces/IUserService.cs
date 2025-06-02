
namespace DbmApi.Interfaces
{
    public interface IUserService
    {
        int Id { get; set; }
        string UserSysname { get; set; }
        string Name { get; set; }
        string Login { get; set; }
        string SystemRole { get; set; }

        // Methods
        void Authenticate(string username, string password);
    }
}
