namespace DbmApi.Models
{
    public class DbmUser
    {
        public int Id { get; set; }
        public string UserSysname { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string SystemRole { get; set; }
        public string Password { get; set; }
    }
}
