using ClientConnectorApi.Configurations;
using ClientConnectorApi.Exceptions;
using MessagingDomain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClientConnectorApi.Services.Authentication
{
    public class JwtService : IJwtService
    {
        private readonly JwtConfiguration _jwtSettings;
        private readonly byte[] _key;

        public JwtService(IOptions<JwtConfiguration> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
            _key = Encoding.UTF8.GetBytes(_jwtSettings.Secret);
        }

        public string GenerateAccessToken(User user)
        {
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: GetClaims(user),
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private IEnumerable<Claim> GetClaims(User user) => new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
            new Claim(ClaimTypes.Name, user.Username)
        };
    }
}
