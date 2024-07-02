using Appointments.Application.Results.Common;
using MediatR;

namespace Appointments.Application.Results.Commands.Create;

public record CreateAppointmentResultCommand(
    Guid AppointmentId,
    string Complaints,
    string Conclusion,
    string Recommendations) : IRequest<ResultResult>;