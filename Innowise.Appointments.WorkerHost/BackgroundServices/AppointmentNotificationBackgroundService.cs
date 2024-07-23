using Appointments.Application.Appointments.Commands.NotifyUpcoming;
using MediatR;

namespace Innowise.Appointments.WorkerHost.BackgroundServices;

public class AppointmentNotificationBackgroundService(IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = serviceScopeFactory.CreateScope();
            var sender = scope.ServiceProvider.GetRequiredService<ISender>();

            await sender.Send(new NotifyUpcomingAppointmentsCommand(), stoppingToken);

            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
        }
    }
}