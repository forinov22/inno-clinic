using Appointments.Application.Appointments.Common;
using MediatR;

namespace Appointments.Application.Appointments.Queries.GetById;

public record GetAppointmentByIdQuery(Guid AppointmentId) : IRequest<AppointmentResult>;