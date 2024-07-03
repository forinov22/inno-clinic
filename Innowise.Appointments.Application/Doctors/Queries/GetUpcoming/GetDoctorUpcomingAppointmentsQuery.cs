using Appointments.Application.Appointments.Common;
using MediatR;

namespace Appointments.Application.Doctors.Queries.GetUpcoming;

public record GetDoctorUpcomingAppointmentsQuery(Guid DoctorId) : IRequest<IEnumerable<AppointmentResult>>;