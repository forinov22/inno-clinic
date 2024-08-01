namespace Appointments.Domain.Entities;

public class Result
{
    public Guid Id { get; set; }
    public string Complaints { get; set; } = string.Empty;
    public string Conclusion { get; set; } = string.Empty;
    public string Recommendations { get; set; } = string.Empty;
    public DateTime DateTime { get; set; }
    public Guid AppointmentId { get; set; }
    public Appointment Appointment { get; set; } = null!;
}