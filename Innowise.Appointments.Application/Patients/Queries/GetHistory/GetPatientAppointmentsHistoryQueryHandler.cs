using Appointments.Application.Appointments.Common;
using Appointments.Application.Interfaces;
using MediatR;

namespace Appointments.Application.Patients.Queries.GetHistory;

internal class GetPatientAppointmentsHistoryQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetPatientAppointmentsHistoryQuery, IEnumerable<AppointmentResult>>
{
    public async Task<IEnumerable<AppointmentResult>> Handle(GetPatientAppointmentsHistoryQuery request, CancellationToken cancellationToken)
    {
        var appointments = await unitOfWork.AppointmentRepository.GetPatientHistoryAsync(request.PatientId);
        return appointments.Select(appointment => appointment.MapToDto()).ToList();
    }
}