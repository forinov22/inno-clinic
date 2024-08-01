using Appointments.Application.Interfaces;
using Appointments.Application.Interfaces.HttpClients.Services;
using Appointments.Infrastructure.HttpClients;
using Appointments.Infrastructure.Pdf;
using Appointments.Infrastructure.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Retry;
using QuestPDF.Infrastructure;

namespace Appointments.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddMassTransit(this IServiceCollection services)
    {
        services.AddMassTransit(config =>
        {
            config.AddEntityFrameworkOutbox<AppointmentsDbContext>(o =>
            {
                o.UsePostgres().UseBusOutbox();
            });

            config.UsingRabbitMq((context, cfg) => { cfg.ConfigureEndpoints(context); });
        });

        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgres")
                            ?? throw new Exception("Connection string not provided");

        services.AddDbContext<AppointmentsDbContext>(options => options.UseNpgsql(connectionString),
                                                     ServiceLifetime.Transient);
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    public static IServiceCollection AddHttpClients(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHttpClient<IServiceHttpClient, ServiceHttpClient>(client =>
        {
            client.BaseAddress =
                new Uri(
                    configuration[
                        "ServiceUrl:ServicesService:BaseUrl"] ??
                    throw new Exception(
                        "ServicesService url is not provided"));
        });

        return services;
    }

    public static IServiceCollection AddPolly(this IServiceCollection services)
    {
        services.AddResiliencePipeline<string>("services-client", pipelineBuilder =>
        {
            pipelineBuilder
            .AddRetry(new RetryStrategyOptions())
            .AddTimeout(TimeSpan.FromSeconds(10));
        });

        return services;
    }

    public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis")
                                 ?? throw new Exception("Redis connection string not provided");
        });

        return services;
    }

    public static IServiceCollection AddPdf(this IServiceCollection services)
    {
        services.AddTransient<IPdfService, PdfService>();
        QuestPDF.Settings.License = LicenseType.Community;

        return services;
    }
}