using ClientConnectorApi.Services.Authentication;
using ClientConnectorApi.Services.Users;
using ClientConnectorApi.Configurations;
using ClientConnectorApi.Services.Registration;

namespace ClientConnectorApi.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection SetupServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtConfiguration>(configuration.GetSection("JwtSettings"));

            services.AddScoped<IJwtService, JwtService>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<IUserDataService, UserDataService>();

            return services;
        }
    }
}
