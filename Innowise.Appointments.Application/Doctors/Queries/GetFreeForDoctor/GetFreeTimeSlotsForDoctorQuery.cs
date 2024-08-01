using Appointments.Application.Doctors.Common;
using MediatR;

namespace Appointments.Application.Doctors.Queries.GetFreeForDoctor;

public record GetFreeTimeSlotsForDoctorQuery(Guid DoctorId, DateTime? Date) : IRequest<IEnumerable<TimeSlotResult>>;