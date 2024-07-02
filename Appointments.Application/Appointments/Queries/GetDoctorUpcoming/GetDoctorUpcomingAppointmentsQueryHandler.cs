using Appointments.Application.Appointments.Common;
using Appointments.Application.Interfaces;
using MediatR;

namespace Appointments.Application.Appointments.Queries.GetDoctorUpcoming;

internal class GetDoctorUpcomingAppointmentsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetDoctorUpcomingAppointmentsQuery, IEnumerable<AppointmentResult>>
{
    public async Task<IEnumerable<AppointmentResult>> Handle(GetDoctorUpcomingAppointmentsQuery request, CancellationToken cancellationToken)
    {
        var appointments = await unitOfWork.AppointmentRepository.GetDoctorUpcomingAsync(request.DoctorId);
        return appointments.Select(AppointmentResult.MapFromAppointment);
    }
}