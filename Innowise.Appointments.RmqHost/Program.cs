using Appointments.Application;
using Appointments.Infrastructure;
using InnoClinic.Services;
using Innowise.Appointments.RmqHost.Consumers;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<DoctorProfileCreatedConsumer>();
    config.AddConsumer<PatientProfileCreatedConsumer>();
    config.AddConsumer<PatientProfileLinkedToAccountConsumer>();
    config.AddConsumer<ServicesUpdatedConsumer>();

    config.UsingRabbitMq((context, cfg) => { cfg.ConfigureEndpoints(context); });
});

builder.Services
       .AddApplication()
       .AddInfrastructure(builder.Configuration)
       .AddEmail(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
