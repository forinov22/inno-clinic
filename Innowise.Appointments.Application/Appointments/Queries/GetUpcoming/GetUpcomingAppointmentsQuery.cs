using Appointments.Application.Appointments.Common;
using MediatR;

namespace Appointments.Application.Appointments.Queries.GetUpcoming;

public record GetUpcomingAppointmentsQuery() : IRequest<IEnumerable<AppointmentResult>>;