using Appointments.Application.Interfaces;
using Innowise.Common.Services.Email;
using MediatR;

namespace Appointments.Application.Appointments.Commands.NotifyUpcoming;

public class NotifyUpcomingAppointmentsCommandHandler(IUnitOfWork unitOfWork, IEmailService emailService) : IRequestHandler<NotifyUpcomingAppointmentsCommand>
{
    public async Task Handle(NotifyUpcomingAppointmentsCommand request, CancellationToken cancellationToken)
    {
        var appointments = await unitOfWork.AppointmentRepository.GetAllStartingTomorrowAsync();

        foreach (var appointment in appointments)
        {
            if (appointment.Patient.Email is null) continue;

            var emailBody = $"""
                             <h1>Don't forget about your appointment scheduled for tomorrow!</h1>
                             <p>Appointment with Dr. {appointment.Doctor.FirstName + ' ' + appointment.Doctor.LastName} at {appointment.StartDate}</p>
                             """;

            await emailService.SendEmailAsync(appointment.Patient.Email, "Appointment Reminder", emailBody);
        }
    }
}