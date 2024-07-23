using Appointments.Application.Appointments.Common;
using Appointments.Application.Appointments.Exceptions;
using Appointments.Application.Extensions;
using Appointments.Application.Interfaces;
using MediatR;

namespace Appointments.Application.Appointments.Queries.GetById;

internal class GetAppointmentByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAppointmentByIdQuery, AppointmentResult>
{
    public async Task<AppointmentResult> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
    {
        var appointment = await unitOfWork.AppointmentRepository.GetByIdAsync(request.AppointmentId);

        if (appointment == null)
        {
            throw new AppointmentNotFoundException();
        }

        return appointment.ToAppointmentResult();
    }
}