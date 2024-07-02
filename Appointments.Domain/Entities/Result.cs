namespace Appointments.Domain.Entities;

public class Result
{
    public Guid Id { get; set; }
    public string Complaints { get; set; }
    public string Conclusion { get; set; }
    public string Recommendations { get; set; }
    public DateTime DateTime { get; set; }
    public Guid AppointmentId { get; set; }
    public Appointment Appointment { get; set; }
}