using ClientConnectorApi.Dtos.Requests;
using ClientConnectorApi.Dtos.Responses;
using ClientConnectorApi.Exceptions;
using ClientConnectorApi.Services.Factories;
using MessagingDomain;
using MessaingData;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ClientConnectorApi.Services.Users
{
    public class UserDataService : IUserDataService
    {
        private readonly MessagingContext _context;

        public UserDataService(MessagingContext context)
        {
            _context = context;
        }


        public async Task<UserDTO> GetUserDataAsync(ClaimsPrincipal userClaimsPrinciple)
        {
            return await TryGetUserDataAsync(userClaimsPrinciple);
        }

        private async Task<UserDTO> TryGetUserDataAsync(ClaimsPrincipal userClaimsPrinciple)
        {
            var userIdClaim = userClaimsPrinciple.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var usernameClaim = userClaimsPrinciple.FindFirst(ClaimTypes.Name)?.Value;

            if (!int.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedException("Invalid user ID format.");
            }

            var userEntity = await _context.Users.Include(u => u.UserData).FirstOrDefaultAsync(u => u.UserId == userId);

            if (userEntity == null)
            {
                throw new NotFoundException("Missing User");
            }

            return UserFactory.EntityToDto(userEntity);
        }
    }
}
