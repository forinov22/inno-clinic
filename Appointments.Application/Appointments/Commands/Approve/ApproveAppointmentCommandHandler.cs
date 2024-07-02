using Appointments.Application.Appointments.Common;
using Appointments.Application.Interfaces;
using Auth.Domain.Exceptions;
using MediatR;

namespace Appointments.Application.Appointments.Commands.Approve;

internal class ApproveAppointmentCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ApproveAppointmentCommand, AppointmentResult>
{
    public async Task<AppointmentResult> Handle(ApproveAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = await unitOfWork.AppointmentRepository.GetByIdAsync(request.AppointmentId);
        if (appointment is null)
        {
            throw new NotFoundException("Appointment not found");
        }

        appointment.IsApproved = true;
        await unitOfWork.SaveAllAsync();

        return AppointmentResult.MapFromAppointment(appointment);
    }
}