using Appointments.Application.Appointments.Common;
using Appointments.Application.Interfaces;
using MediatR;

namespace Appointments.Application.Appointments.Queries.GetUpcoming;

internal class GetUpcomingAppointmentsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetUpcomingAppointmentsQuery, IEnumerable<AppointmentResult>>
{
    public async Task<IEnumerable<AppointmentResult>> Handle(GetUpcomingAppointmentsQuery request, CancellationToken cancellationToken)
    {
        var appointments = await unitOfWork.AppointmentRepository.GetAllUpcomingAsync();
        return appointments.Select(appointment => appointment.MapToDto()).ToList();
    }
}