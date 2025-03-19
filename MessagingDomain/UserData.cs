namespace MessagingDomain
{
    public class UserData
    {
        public int UserDataId { get; set; }
        public required string DisplayName { get; set; }
        public required string Email { get; set; }
        public required string Address { get; set; }
    }
}
