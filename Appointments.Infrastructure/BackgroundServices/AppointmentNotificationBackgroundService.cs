using Appointments.Application.Interfaces;
using InnoClinic.Services.Email;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Appointments.Infrastructure.BackgroundServices;

public class AppointmentNotificationBackgroundService(IServiceScopeFactory serviceScopeFactory)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = serviceScopeFactory.CreateScope();
            
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
            
            var appointments = await unitOfWork.AppointmentRepository.GetAllStartingTomorrowAsync();

            foreach (var appointment in appointments)
            {
                if (appointment.Patient.Email is null) continue;
                
                var emailBody = $"""
                                 <h1>Dont forget about your appointment scheduled for tomorrow!</h1>
                                 <p>Appointment with Dr. {appointment.Doctor.FirstName + ' ' + appointment.Doctor.LastName} at {appointment.StartDate}</p>
                                 """;
                
                await emailService.SendEmailAsync(appointment.Patient.Email, "Appointment Reminder", emailBody);
            }

            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
        }
    }
}