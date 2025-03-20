using MessagingDomain;

namespace ClientConnectorApi.Dtos.Responses
{
    public record UserDTO(
        int userId,
        string username,
        UserDataDTO userData)
    {
        public UserDTO(User userEntity) 
            : this(
                  userEntity.UserId,
                  userEntity.Username,
                  new UserDataDTO(
                      userEntity.UserData.UserDataId,
                      userEntity.UserData.DisplayName,
                      userEntity.UserData.Email,
                      userEntity.UserData.Address)) { }
    }
}
