using Innowise.Profiles.RmqHost;
using Innowise.Profiles.RmqHost.Consumers;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Profiles.Application;
using Profiles.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddMassTransit(config =>
{
    config.AddEntityFrameworkOutbox<OutboxDbContext>(o =>
    {
        o.UsePostgres();
    });

    config.AddConsumer<PatientAccountCreatedConsumer>();

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
