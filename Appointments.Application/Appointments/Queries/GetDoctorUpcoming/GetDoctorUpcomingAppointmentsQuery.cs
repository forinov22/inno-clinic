using Appointments.Application.Appointments.Common;
using MediatR;

namespace Appointments.Application.Appointments.Queries.GetDoctorUpcoming;

public record GetDoctorUpcomingAppointmentsQuery(Guid DoctorId) : IRequest<IEnumerable<AppointmentResult>>;