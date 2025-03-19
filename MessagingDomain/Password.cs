namespace MessagingDomain
{
    public class Password
    {
        public int PasswordId { get; set; }
        public required string HashedValue { get; set; }
    }
}
