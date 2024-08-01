using Appointments.Application.Appointments.Common;
using MediatR;

namespace Appointments.Application.Appointments.Commands.UpdateResult;

public record UpdateAppointmentResultCommand(
    Guid AppointmentId,
    string Complaints,
    string Conclusion,
    string Recommendations) : IRequest<ResultResult>;