using Appointments.Domain.Entities;

namespace Appointments.Application.Appointments.Common;

public static class ResultResultExtensions
{
    public static ResultResult MapToDto(this Result result)
    {
        return new ResultResult(
            result.Id,
            result.Complaints,
            result.Recommendations,
            result.AppointmentId,
            result.DateTime,
            result.Appointment.PatientId,
            result.Appointment.Patient.FirstName,
            result.Appointment.Patient.LastName,
            result.Appointment.Patient.MiddleName,
            result.Appointment.DoctorId,
            result.Appointment.Doctor.FirstName,
            result.Appointment.Doctor.LastName,
            result.Appointment.Doctor.MiddleName,
            result.Appointment.ServiceId,
            result.Appointment.Service.ServiceName);
    }
}