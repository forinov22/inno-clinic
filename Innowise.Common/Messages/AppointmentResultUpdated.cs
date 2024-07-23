using Appointments.Domain.Entities;

namespace Innowise.Common.Messages;

public class AppointmentResultUpdated
{
    public string Email { get; set; }
    public Result Result { get; set; }
}