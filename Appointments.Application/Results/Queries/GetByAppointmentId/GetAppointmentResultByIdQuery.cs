using Appointments.Application.Results.Common;
using MediatR;

namespace Appointments.Application.Results.Queries.GetByAppointmentId;

public record GetAppointmentResultByIdQuery(Guid AppointmentId) : IRequest<ResultResult>;