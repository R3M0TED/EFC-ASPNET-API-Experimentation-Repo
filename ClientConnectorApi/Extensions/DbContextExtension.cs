using Microsoft.EntityFrameworkCore;
using MessaingData;

namespace ClientConnectorApi.Extensions
{
    public static class DbContextExtension
    {
        public static IServiceCollection SetupDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MessagingContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MessagingConnection"))
                    .EnableSensitiveDataLogging()
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            );

            return services;
        }
    }
}
