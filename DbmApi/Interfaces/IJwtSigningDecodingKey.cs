using Microsoft.IdentityModel.Tokens;

namespace DbmApi.Interfaces
{
    public interface IJwtSigningDecodingKey
    {
        SecurityKey GetKey();
    }
}
