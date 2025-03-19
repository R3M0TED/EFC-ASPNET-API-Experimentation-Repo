using MessagingDomain;

namespace ClientConnectorApi.Dtos.Requests
{
    public record UserRegistrationDTO(
        string Username,
        string DisplayName,
        string Email,
        string Address,
        string Password);
}
