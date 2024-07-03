using Appointments.Application.Appointments.Commands.Approve;
using Appointments.Application.Appointments.Commands.Cancel;
using Appointments.Application.Appointments.Commands.Create;
using Appointments.Application.Appointments.Commands.UpdateResult;
using Appointments.Application.Appointments.Common;
using Appointments.Application.Appointments.Queries.GetById;
using Appointments.Application.Appointments.Queries.GetResultByAppointmentId;
using Appointments.Application.Appointments.Queries.GetUpcoming;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Appointments.API.Controllers;

[Route("api/appointments")]
[ApiController]
public class AppointmentController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppointmentResult>>> ListUpcoming()
    {
        var appointments = await sender.Send(new GetUpcomingAppointmentsQuery());
        return appointments.ToList();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AppointmentResult>> GetById([FromRoute] Guid id)
    {
        var appointment = await sender.Send(new GetAppointmentByIdQuery(id));
        return appointment;
    }

    [HttpPost]
    public async Task<ActionResult<AppointmentResult>> Create(
        [FromBody] CreateAppointmentCommand createAppointmentCommand)
    {
        var appointment = await sender.Send(createAppointmentCommand);
        return CreatedAtAction(nameof(GetById), new { id = appointment.Id }, appointment);
    }

    [HttpPost("{id:guid}:approve")]
    public async Task<ActionResult<AppointmentResult>> Approve([FromRoute] Guid id)
    {
        var appointment = await sender.Send(new ApproveAppointmentCommand(id));
        return CreatedAtAction(nameof(GetById), new { appointmentId = id }, appointment);
    }

    [HttpPost("{id:guid}:cancel")]
    public async Task<ActionResult<AppointmentResult>> Cancel([FromRoute] Guid id)
    {
        await sender.Send(new CancelAppointmentCommand(id));
        return NoContent();
    }
    
    [HttpGet("{appointmentId:guid}/result")]
    public async Task<ActionResult<ResultResult>> GetAppointmentResultById([FromRoute] Guid appointmentId)
    {
        var query = new GetResultByAppointmentIdQuery(appointmentId);
        var result = await sender.Send(query);
        return Ok(result);
    }
    
    [HttpPatch("{appointmentId:guid}/result")]
    public async Task<ActionResult<ResultResult>> CreateAppointmentResult([FromRoute] Guid appointmentId, [FromBody] UpdateAppointmentResultCommand command)
    {
        if (appointmentId != command.AppointmentId)
        {
            return BadRequest("Appointment ID mismatch");
        }
        
        var result = await sender.Send(command);
        return CreatedAtAction(nameof(GetAppointmentResultById), new { appointmentId = result.AppointmentId }, result);
    }
}