using Appointments.Application;
using Appointments.Infrastructure;
using InnoClinic.Services;
using Innowise.Appointments.WorkerHost;
using Innowise.Appointments.WorkerHost.BackgroundServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddHostedService<AppointmentNotificationBackgroundService>();
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