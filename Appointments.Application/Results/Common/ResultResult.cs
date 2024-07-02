using Appointments.Domain.Entities;

namespace Appointments.Application.Results.Common;

public class ResultResult(
    Guid id,
    string complaints,
    string recommendations,
    DateTime dateTime,
    Guid patientId,
    string patientFirstName,
    string patientLastName,
    string? patientMiddleName,
    Guid doctorId,
    string doctorFirstName,
    string doctorLastName,
    string? doctorMiddleName,
    Guid serviceId,
    string serviceName)
{
    public Guid Id { get; set; } = id;
    public string Complaints { get; set; } = complaints;
    public string Recommendations { get; set; } = recommendations;
    public DateTime DateTime { get; set; } = dateTime;
    public Guid PatientId { get; set; } = patientId;
    public string PatientFirstName { get; set; } = patientFirstName;
    public string PatientLastName { get; set; } = patientLastName;
    public string? PatientMiddleName { get; set; } = patientMiddleName;
    public Guid DoctorId { get; set; } = doctorId;
    public string DoctorFirstName { get; set; } = doctorFirstName;
    public string DoctorLastName { get; set; } = doctorLastName;
    public string? DoctorMiddleName { get; set; } = doctorMiddleName;
    public Guid ServiceId { get; set; } = serviceId;
    public string ServiceName { get; set; } = serviceName;

    public static ResultResult MapFromResult(Result result)
    {
        return new ResultResult(
            result.Id,
            result.Complaints,
            result.Recommendations,
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