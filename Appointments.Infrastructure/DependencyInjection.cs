using Appointments.Application.Interfaces;
using Appointments.Infrastructure.BackgroundServices;
using Appointments.Infrastructure.Consumers;
using Appointments.Infrastructure.Pdf;
using Appointments.Infrastructure.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuestPDF.Infrastructure;
using Serilog;

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

        services.AddMassTransit(config =>
        {
            config.AddConsumer<DoctorProfileCreatedConsumer>();
            config.AddConsumer<PatientProfileCreatedConsumer>();
            config.AddConsumer<PatientProfileLinkedToAccountConsumer>();
            config.AddConsumer<ServicesUpdatedConsumer>();

            config.UsingRabbitMq((context, cfg) => { cfg.ConfigureEndpoints(context); });
        });

        services.AddTransient<IPdfService, PdfService>();
        QuestPDF.Settings.License = LicenseType.Community;

        services.AddHostedService<AppointmentNotificationBackgroundService>();

        
        // services.AddSerilog();
        
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