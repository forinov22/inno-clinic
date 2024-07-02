using Appointments.Application.TimeSlots.Common;
using Appointments.Application.TimeSlots.Queries.GetFreeForDoctor;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Appointments.API.Controllers;

[Route("api/time-slots")]
[ApiController]
public class TimeSlotController(ISender sender) : ControllerBase
{
    [HttpGet("doctor/{doctorId:guid}")]
    public async Task<ActionResult<IEnumerable<TimeSlotResult>>> GetFreeTimeSlotsForDoctor([FromRoute] Guid doctorId,
        [FromQuery] DateTime? date)
    {
        var timeSlots = await sender.Send(new GetFreeTimeSlotsForDoctorQuery(doctorId, date));
        return timeSlots.ToList();
    }
}