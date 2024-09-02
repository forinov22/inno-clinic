using Appointments.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using Testcontainers.Redis;

namespace Innowise.Appointments.Tests.Integration;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("appointments")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    private readonly RedisContainer _redisContainer = new RedisBuilder()
        .WithImage("redis:latest")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test");

        builder.ConfigureAppConfiguration((context, config) =>
        {
            var integrationConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Test.json")
                .Build();

            config.AddConfiguration(integrationConfig);
        });

        builder.ConfigureTestServices(services =>
        {
            var descriptor = services.SingleOrDefault(s
                => s.ServiceType == typeof(DbContextOptions<AppointmentsDbContext>));

            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<AppointmentsDbContext>(options
                => options.UseNpgsql(_dbContainer.GetConnectionString()));

            services.AddStackExchangeRedisCache(options
                => options.Configuration = _redisContainer.GetConnectionString());
        });
    }

    public async Task InitializeAsync()
    {
        await _redisContainer.StartAsync();
        await _dbContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _redisContainer.StopAsync();
        await _dbContainer.StopAsync();
    }
}