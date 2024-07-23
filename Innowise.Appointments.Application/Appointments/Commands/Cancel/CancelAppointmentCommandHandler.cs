using Appointments.Application.Appointments.Exceptions;
using Appointments.Application.Interfaces;
using MediatR;

namespace Appointments.Application.Appointments.Commands.Cancel;

internal class CancelAppointmentCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CancelAppointmentCommand>
{
    public async Task Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = await unitOfWork.AppointmentRepository.GetByIdAsync(request.AppointmentId);
        if (appointment is null)
        {
            throw new AppointmentNotFoundException();
        }

        unitOfWork.AppointmentRepository.Remove(appointment);
        await unitOfWork.SaveAllAsync();
    }
}