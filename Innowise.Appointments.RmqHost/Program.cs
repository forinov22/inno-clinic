using Appointments.Application;
using Appointments.Infrastructure;
using Innowise.Appointments.RmqHost;
using Innowise.Appointments.RmqHost.Consumers;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddMassTransit(config =>
{
    config.AddEntityFrameworkOutbox<OutboxDbContext>(o =>
    {
        o.UsePostgres();
    });

    config.AddConsumer<DoctorProfileCreatedConsumer>();
    config.AddConsumer<PatientProfileCreatedConsumer>();
    config.AddConsumer<PatientProfileLinkedToAccountConsumer>();

    config.UsingRabbitMq((context, cfg) =>
    {
        cfg.UseMessageRetry(r => r.Immediate(5));
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddDbContext<OutboxDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("Postgres")
        ?? throw new NullReferenceException("Connection string is not provided");

    options.UseNpgsql(connectionString);
});

builder.Services
       .AddApplication()
       .AddPersistence(builder.Configuration)
       .AddRedis(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();

app.Run();