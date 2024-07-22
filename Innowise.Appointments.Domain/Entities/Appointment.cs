namespace Appointments.Domain.Entities;

public class Appointment
{
    public const int TimeSlotSize = 10;

    public Guid Id { get; set; }
    public DateTime StartDate { get; set; }
    public bool IsApproved { get; set; }
    public Guid PatientId { get; set; }
    public Patient Patient { get; set; }
    public Guid DoctorId { get; set; }
    public Doctor Doctor { get; set; }
    public Guid ServiceId { get; set; }
    public Service Service { get; set; }
}