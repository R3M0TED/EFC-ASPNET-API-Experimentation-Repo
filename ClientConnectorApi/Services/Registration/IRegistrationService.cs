using ClientConnectorApi.Dtos.Requests;
using ClientConnectorApi.Dtos.Responses;

namespace ClientConnectorApi.Services.Registration
{
    public interface IRegistrationService
    {
        Task<UserDTO> RegisterUserAsync(UserRegistrationDTO registerationDto);
    }
}
