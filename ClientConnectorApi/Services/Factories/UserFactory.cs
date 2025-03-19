using ClientConnectorApi.Dtos.Requests;
using ClientConnectorApi.Dtos.Responses;
using MessagingDomain;

namespace ClientConnectorApi.Services.Factories
{
    public class UserFactory
    {
        public static User DtoToEntity(UserRegistrationDTO dto)
        {
            return new User
            {
                Username = dto.Username,
                UserData = new UserData { Address = dto.Address, DisplayName = dto.DisplayName, Email = dto.Email},
                Password = new Password { HashedValue = PasswordFactory.HashPassword(dto.Password) }
            };
        }

        public static UserDTO EntityToDto(User userEntity)
        {
            return new UserDTO(userEntity);
        }
    }
}
