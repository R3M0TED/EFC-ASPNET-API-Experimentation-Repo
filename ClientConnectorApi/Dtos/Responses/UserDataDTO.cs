using MessagingDomain;

namespace ClientConnectorApi.Dtos.Responses
{
    public record UserDataDTO(
        int userDataId,
        string DisplayName,
        string email,
        string address)
    {
        public UserDataDTO(UserData userDataEntity) 
            : this(
                  userDataEntity.UserDataId,
                  userDataEntity.DisplayName,
                  userDataEntity.Email,
                  userDataEntity.Address) { }
    }
}
