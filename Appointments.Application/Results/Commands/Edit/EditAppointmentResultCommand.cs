using Appointments.Application.Results.Common;
using MediatR;

namespace Appointments.Application.Results.Commands.Edit;

public record EditAppointmentResultCommand(
    Guid ResultId,
    string Complaints,
    string Conclusion,
    string Recommendations) : IRequest<ResultResult>;