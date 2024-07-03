using Appointments.Application.Interfaces;
using Appointments.Infrastructure.Pdf;
using Appointments.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuestPDF.Infrastructure;

namespace Appointments.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis")
                                 ?? throw new Exception("Redis connection string not provided");
        });

        services.AddTransient<IPdfService, PdfService>();
        QuestPDF.Settings.License = LicenseType.Community;
        
        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgres")
                            ?? throw new Exception("Connection string not provided");

        services.AddDbContext<AppointmentsDbContext>(options => options.UseNpgsql(connectionString),
                                                     ServiceLifetime.Transient);
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}