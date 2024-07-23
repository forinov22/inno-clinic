using Appointments.Application.Appointments.Common;
using Appointments.Application.Appointments.Exceptions;
using Appointments.Application.Extensions;
using Appointments.Application.Interfaces;
using Appointments.Domain.Entities;
using MediatR;

namespace Appointments.Application.Appointments.Commands.Approve;

internal class ApproveAppointmentCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ApproveAppointmentCommand>
{
    public async Task Handle(ApproveAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = await unitOfWork.AppointmentRepository.GetByIdAsync(request.AppointmentId);
        if (appointment is null)
        {
            throw new AppointmentNotFoundException();
        }

        appointment.IsApproved = true;
        await unitOfWork.SaveAllAsync();
    }
}