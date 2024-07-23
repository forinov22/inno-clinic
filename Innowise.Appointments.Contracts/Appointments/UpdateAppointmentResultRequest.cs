namespace Innowise.Appointments.Contracts.Appointments;

public record UpdateAppointmentResultRequest(
    string Complaints,
    string Conclusion,
    string Recommendations);