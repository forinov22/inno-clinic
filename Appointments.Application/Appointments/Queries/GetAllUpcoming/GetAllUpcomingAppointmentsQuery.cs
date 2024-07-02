using Appointments.Application.Appointments.Common;
using MediatR;

namespace Appointments.Application.Appointments.Queries.GetAllUpcoming;

public record GetAllUpcomingAppointmentsQuery() : IRequest<IEnumerable<AppointmentResult>>;