using Appointments.Application.TimeSlots.Common;
using MediatR;

namespace Appointments.Application.TimeSlots.Queries.GetFreeForDoctor;

public record GetFreeTimeSlotsForDoctorQuery(Guid DoctorId, DateTime? Date) : IRequest<IEnumerable<TimeSlotResult>>;