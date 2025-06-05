namespace DbmApi.Models
{
    public class LoginRequest
    {
        public required string Login { get; set; }
        public required string SystemName { get; set; }
        public required string Password { get; set; }
        public string? SystemRole   { get; set; }
    }
}
