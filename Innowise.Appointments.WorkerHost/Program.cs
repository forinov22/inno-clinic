using Appointments.Application;
using Appointments.Infrastructure;
using Innowise.Appointments.WorkerHost;
using Innowise.Appointments.WorkerHost.BackgroundServices;
using Innowise.Common.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddHostedService<AppointmentNotificationBackgroundService>();
builder.Services
       .AddApplication()
       .AddPersistence(builder.Configuration)
       .AddRedis(builder.Configuration)
       .AddEmail(builder.Configuration.GetSection("EmailOptions").Bind);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();