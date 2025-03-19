using ClientConnectorApi.Dtos.Requests;
using MessaingData;
using Microsoft.EntityFrameworkCore;
using ClientConnectorApi.Exceptions;
using MessagingDomain;
using ClientConnectorApi.Dtos.Responses;

namespace ClientConnectorApi.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly MessagingContext _context;
        private readonly IJwtService _jwtService;
        public AuthenticationService(MessagingContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<TokenDTO> AuthenticateAsync(UserAuthenticationDTO authenticationDto)
        {
            var userEntity = await TryAuthenticateAsync(authenticationDto);
            var token = _jwtService.GenerateAccessToken(userEntity);

            return token;
        }


        private async Task<User> TryAuthenticateAsync(UserAuthenticationDTO authenticationDto)
        {
            var userEntity = await _context.Users.Include(u => u.Password).FirstOrDefaultAsync(x => x.Username == authenticationDto.Username);

            if (userEntity == null || !BCrypt.Net.BCrypt.Verify(authenticationDto.Password, userEntity.Password.HashedValue))
            {
                throw new UnauthorizedException("Invalid username or password");
            }

            return userEntity;
        }
    }
}
