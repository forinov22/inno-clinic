using Appointments.Domain.Entities;

namespace Appointments.Application.Appointments.Common;

public static class AppointmentResultExtensions
{
    public static AppointmentResult MapToDto(this Appointment appointment)
    {
        return new AppointmentResult(
            appointment.Id,
            appointment.Patient.ExternalId,
            appointment.Patient.FirstName,
            appointment.Patient.LastName,
            appointment.Patient.MiddleName,
            appointment.ServiceId,
            appointment.Service.ServiceName,
            appointment.Doctor.FirstName,
            appointment.Doctor.LastName,
            appointment.Doctor.MiddleName,
            appointment.StartDate,
            appointment.StartDate.Add(TimeSpan.FromMinutes(Appointment.TimeSlotSize * appointment.Service.TimeSlotSize)));
    }
}