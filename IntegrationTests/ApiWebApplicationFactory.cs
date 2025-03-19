using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using MessaingData;

public class ApiWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    private readonly IConfiguration _configuration;
    private readonly bool _deleteDatabase;
    private readonly string _connectionString;

    public ApiWebApplicationFactory()
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(System.IO.Directory.GetCurrentDirectory())
            .AddJsonFile("testsettings.json", optional: false, reloadOnChange: true)
            .Build();

        _connectionString = _configuration.GetValue<string>("TestSettings:ConnectionString")
                            ?? throw new InvalidOperationException("ConnectionString is missing in testsettings.json.");

        _deleteDatabase = _configuration.GetValue<bool?>("TestSettings:DeleteDatabase") ?? false;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices((context, services) =>
        {
            services.AddDbContext<MessagingContext>(options =>
                options.UseSqlServer(_connectionString)
                       .EnableSensitiveDataLogging()
                       .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            using var scope = services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<MessagingContext>();

            dbContext.Database.EnsureDeleted();

            dbContext.Database.EnsureCreated();
        });
    }

    public void CleanUp()
    {
        if (!_deleteDatabase) return;

        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<MessagingContext>();
        dbContext.Database.EnsureDeleted();
    }
}
