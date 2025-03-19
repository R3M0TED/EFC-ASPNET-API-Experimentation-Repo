using ClientConnectorApi.Dtos.Requests;
using ClientConnectorApi.Dtos.Responses;

namespace ClientConnectorApi.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<string> AuthenticateAsync(UserAuthenticationDTO authenticationDto);
    }
}
