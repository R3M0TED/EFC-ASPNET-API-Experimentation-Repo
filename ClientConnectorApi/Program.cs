using ClientConnectorApi.Extensions;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.SetupSwagger();

        builder.Services.SetupDbContext(builder.Configuration);
        builder.Services.SetupConfigurations(builder.Configuration);
        builder.Services.SetupServices(builder.Configuration);
        builder.Services.AddJwtAuthentication(builder.Configuration);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwaggerWithAuth();
        }

        app.UseHttpsRedirection();

        app.MapControllers();

        app.Run();
    }
}
