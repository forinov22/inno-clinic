namespace Appointments.Application.Appointments.Common;

public class ResultResult(
    Guid id,
    string complaints,
    string recommendations,
    Guid appointmentId,
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
    public Guid AppointmentId { get; set; }
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
}