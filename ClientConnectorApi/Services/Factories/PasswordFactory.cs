namespace ClientConnectorApi.Services.Factories
{
    public class PasswordFactory
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
