using Appointments.Domain.Entities;

namespace InnoClinic.Contracts;

public class AppointmentResultUpdated
{
    public string Email { get; set; }
    public Result Result { get; set; }
}