using ClientConnectorApi.Dtos.Responses;
using MessagingDomain;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace ClientConnectorApi.Services.Authentication
{
    public interface IJwtService
    {
        public TokenDTO GenerateAccessToken(User user);
    }
}
