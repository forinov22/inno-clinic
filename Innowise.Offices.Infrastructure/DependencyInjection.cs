using Innowise.Offices.Application.Interfaces;
using Innowise.Offices.Application.Interfaces.HttpClients.Documents;
using Innowise.Offices.Infrastructure.HttpClients;
using Innowise.Offices.Infrastructure.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Retry;

namespace Innowise.Offices.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(config =>
        {
            config.UsingRabbitMq((context, cfg) => { cfg.ConfigureEndpoints(context); });
        });

        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgres")
                            ?? throw new Exception("Connection string not provided");

        services.AddDbContext<OfficesDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IDocumentHttpClient, DocumentHttpClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["ServiceUrl:DocumentsService:BaseUrl"] ??
                                         throw new Exception("DocumentsService url is not provided"));
        });

        return services;
    }

    public static IServiceCollection AddPolly(this IServiceCollection services)
    {
        services.AddResiliencePipeline<string>("documents-client", pipelineBuilder =>
        {
            pipelineBuilder
                .AddRetry(new RetryStrategyOptions())
                .AddTimeout(TimeSpan.FromSeconds(10));
        });

        return services;
    }
}