using Appointments.Application.Results.Commands.Create;
using Appointments.Application.Results.Commands.Edit;
using Appointments.Application.Results.Common;
using Appointments.Application.Results.Queries.GetByAppointmentId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Appointments.API.Controllers;

[Route("api/results")]
[ApiController]
public class ResultController(ISender sender) : ControllerBase
{
    [HttpGet("{appointmentId:guid}")]
    public async Task<ActionResult<ResultResult>> GetAppointmentResultById([FromRoute] Guid appointmentId)
    {
        var query = new GetAppointmentResultByIdQuery(appointmentId);
        var result = await sender.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ResultResult>> CreateAppointmentResult([FromBody] CreateAppointmentResultCommand command)
    {
        var result = await sender.Send(command);
        //return CreatedAtAction(nameof(GetAppointmentResultById), new { appointmentId = result.AppointmentId }, result);
        return result;
    }

    [HttpPut("{resultId:guid}")]
    public async Task<ActionResult<ResultResult>> EditAppointmentResult([FromRoute] Guid resultId, [FromBody] EditAppointmentResultCommand command)
    {
        if (resultId != command.ResultId)
        {
            return BadRequest("Result ID mismatch");
        }

        var result = await sender.Send(command);
        return Ok(result);
    }
}