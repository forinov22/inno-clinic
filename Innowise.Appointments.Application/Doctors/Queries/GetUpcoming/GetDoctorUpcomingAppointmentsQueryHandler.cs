using Appointments.Application.Appointments.Common;
using Appointments.Application.Extensions;
using Appointments.Application.Interfaces;
using MediatR;

namespace Appointments.Application.Doctors.Queries.GetUpcoming;

internal class GetDoctorUpcomingAppointmentsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetDoctorUpcomingAppointmentsQuery, IEnumerable<AppointmentResult>>
{
    public async Task<IEnumerable<AppointmentResult>> Handle(GetDoctorUpcomingAppointmentsQuery request, CancellationToken cancellationToken)
    {
        var appointments = await unitOfWork.AppointmentRepository.GetDoctorUpcomingAsync(request.DoctorId);
        return appointments.Select(appointment => appointment.ToAppointmentResult()).ToList();
    }
}