using ClientConnectorApi.Configurations;

namespace ClientConnectorApi.Extensions
{
    public static class ConfigurationsExtension
    {
        public static IServiceCollection SetupConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtConfiguration>(configuration.GetSection("JwtSettings"));

            return services;
        }
    }
}
