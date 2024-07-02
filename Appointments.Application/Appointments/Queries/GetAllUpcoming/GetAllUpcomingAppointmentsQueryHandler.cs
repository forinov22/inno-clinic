using Appointments.Application.Appointments.Common;
using Appointments.Application.Interfaces;
using MediatR;

namespace Appointments.Application.Appointments.Queries.GetAllUpcoming;

internal class GetAllUpcomingAppointmentsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllUpcomingAppointmentsQuery, IEnumerable<AppointmentResult>>
{
    public async Task<IEnumerable<AppointmentResult>> Handle(GetAllUpcomingAppointmentsQuery request, CancellationToken cancellationToken)
    {
        var appointments = await unitOfWork.AppointmentRepository.GetAllUpcomingAsync();
        return appointments.Select(AppointmentResult.MapFromAppointment);
    }
}