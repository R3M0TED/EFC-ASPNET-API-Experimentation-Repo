using ClientConnectorApi.Dtos.Responses;
using System.Security.Claims;

namespace ClientConnectorApi.Services.Users
{
    public interface IUserDataService
    {
        public Task<UserDTO> GetUserDataAsync(ClaimsPrincipal userClaimsPrinciple);
    }
}
