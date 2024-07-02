using Appointments.Application.Appointments.Common;
using MediatR;

namespace Appointments.Application.Appointments.Queries.GetPatientHistory;

public record GetPatientAppointmentsHistoryQuery(Guid PatientId) : IRequest<IEnumerable<AppointmentResult>>;