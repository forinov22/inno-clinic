using Appointments.Application.Appointments.Common;
using MediatR;

namespace Appointments.Application.Appointments.Commands.Create;

public record CreateAppointmentCommand(
    Guid PatientId,
    Guid DoctorId,
    Guid ServiceId,
    DateTime StartDate) : IRequest<AppointmentResult>;