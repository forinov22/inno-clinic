using Appointments.Application.Appointments.Commands.NotifyUpcoming;
using MediatR;

namespace Innowise.Appointments.WorkerHost.BackgroundServices;

public class AppointmentNotificationBackgroundService(IServiceScopeFactory serviceScopeFactory, IConfiguration configuration) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var delayInDaysStr = configuration["AppointmentNotificationBackgroundService:DelayInDays"]
            ?? throw new NullReferenceException("Delay not provided");

        if (!int.TryParse(delayInDaysStr, out var delayInDays))
        {
            throw new ArgumentException("Delay provided in incorrect format");
        }

        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = serviceScopeFactory.CreateScope();
            var sender = scope.ServiceProvider.GetRequiredService<ISender>();

            await sender.Send(new NotifyUpcomingAppointmentsCommand(), stoppingToken);

            await Task.Delay(TimeSpan.FromDays(delayInDays), stoppingToken);
        }
    }
}