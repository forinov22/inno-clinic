using Appointments.Application.Appointments.Exceptions;
using Appointments.Application.Interfaces;
using MediatR;

namespace Appointments.Application.Appointments.Commands.Approve;

internal class ApproveAppointmentCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ApproveAppointmentCommand, Guid>
{
    public async Task<Guid> Handle(ApproveAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = await unitOfWork.AppointmentRepository.GetByIdAsync(request.AppointmentId);
        if (appointment is null)
        {
            throw new AppointmentNotFoundException();
        }

        var doctorAppointmentsForDay =
            await unitOfWork.AppointmentRepository.GetDoctorDateAsync(appointment.DoctorId,
                appointment.StartDate.Date);

        var booked = doctorAppointmentsForDay
            .Any(a => a.StartDate >= appointment.StartDate && a.StartDate <=
                      appointment.StartDate.Add(TimeSpan.FromMinutes(appointment.Service.TimeSlotSize))
                      || a.StartDate.Add(TimeSpan.FromMinutes(a.Service.TimeSlotSize)) >=
                      appointment.StartDate &&
                      a.StartDate.Add(TimeSpan.FromMinutes(a.Service.TimeSlotSize)) <=
                      appointment.StartDate.Add(TimeSpan.FromMinutes(appointment.Service.TimeSlotSize)));

        if (booked)
        {
            throw new TimeSlotIsAlreadyInUseException();
        }

        appointment.IsApproved = true;
        await unitOfWork.SaveAllAsync();

        return appointment.Id;
    }
}