using Appointments.Application.Appointments.Common;
using MediatR;

namespace Appointments.Application.Appointments.Commands.Approve;

public record ApproveAppointmentCommand(Guid AppointmentId) : IRequest<Guid>;