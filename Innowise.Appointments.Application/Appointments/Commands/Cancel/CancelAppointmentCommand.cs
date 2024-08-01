using MediatR;

namespace Appointments.Application.Appointments.Commands.Cancel;

public record CancelAppointmentCommand(Guid AppointmentId) : IRequest;