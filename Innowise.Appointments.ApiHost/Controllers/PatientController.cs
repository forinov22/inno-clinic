using Appointments.Application.Appointments.Common;
using Appointments.Application.Patients.Queries.GetHistory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Appointments.API.Controllers;

[ApiController]
[Route("api/patients")]
public class PatientController(ISender sender)
{
    [HttpGet("{id:guid}/appointments")]
    public async Task<ActionResult<IEnumerable<AppointmentResult>>> ListHistory([FromRoute] Guid id)
    {
        var appointments = await sender.Send(new GetPatientAppointmentsHistoryQuery(id));
        return appointments.ToList();
    }
}