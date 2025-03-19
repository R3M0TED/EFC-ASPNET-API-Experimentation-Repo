using MessagingDomain;
using NuGet.Common;

namespace ClientConnectorApi.Dtos.Responses
{
    public record TokenDTO(
        string token,
        DateTime expirationTime)
    {
    }
}
