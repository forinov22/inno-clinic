using Appointments.Application.Appointments.Common;
using Appointments.Application.Patients.Queries.GetHistory;
using Innowise.Appointments.Contracts.Appointments;
using Innowise.Appointments.Contracts.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Appointments.API.Controllers;

[ApiController]
[Route("api/patients")]
public class PatientController(ISender sender)
{
    [HttpGet("{id:guid}/appointments")]
    public async Task<ActionResult<IEnumerable<AppointmentResponse>>> ListHistory([FromRoute] Guid id)
    {
        var appointments = await sender.Send(new GetPatientAppointmentsHistoryQuery(id));
        return appointments.Select(appointment => appointment.ToAppointmentResponse()).ToList();
    }
}