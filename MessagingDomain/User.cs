namespace MessagingDomain
{
    public class User
    {
        public int UserId { get; set; }
        public required string Username { get; set; }
        public required UserData UserData { get; set; }
        public required Password Password { get; set; }
    }
}
