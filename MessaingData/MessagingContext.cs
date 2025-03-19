using MessagingDomain;
using Microsoft.EntityFrameworkCore;

namespace MessaingData
{
    public class MessagingContext : DbContext
    {
        public MessagingContext(DbContextOptions options) 
            : base(options) 
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Password> Passwords { get; set; }
        public DbSet<UserData> UserData { get; set; }
    }
}
