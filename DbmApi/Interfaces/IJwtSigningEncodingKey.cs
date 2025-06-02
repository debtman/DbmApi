using Microsoft.IdentityModel.Tokens;

namespace DbmApi.Interfaces
{
    public interface IJwtSigningEncodingKey
    {
        string SigningAlgorithm { get; }
        SecurityKey GetKey();
    }
}
