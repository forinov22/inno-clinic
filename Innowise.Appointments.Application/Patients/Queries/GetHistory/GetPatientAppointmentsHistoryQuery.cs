using Appointments.Application.Appointments.Common;
using MediatR;

namespace Appointments.Application.Patients.Queries.GetHistory;

public record GetPatientAppointmentsHistoryQuery(Guid PatientId) : IRequest<IEnumerable<AppointmentResult>>;