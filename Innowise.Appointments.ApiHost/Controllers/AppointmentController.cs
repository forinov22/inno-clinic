using Appointments.Application.Appointments.Commands.Approve;
using Appointments.Application.Appointments.Commands.Cancel;
using Appointments.Application.Appointments.Common;
using Appointments.Application.Appointments.Queries.GetById;
using Appointments.Application.Appointments.Queries.GetResultByAppointmentId;
using Appointments.Application.Appointments.Queries.GetUpcoming;
using Innowise.Appointments.Contracts.Appointments;
using Innowise.Appointments.Contracts.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Appointments.API.Controllers;

[Route("api/appointments")]
[ApiController]
public class AppointmentController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppointmentContract>>> ListUpcoming()
    {
        var appointments = await sender.Send(new GetUpcomingAppointmentsQuery());
        return appointments.Select(appointment => appointment.ToAppointmentContract()).ToList();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AppointmentContract>> GetById([FromRoute] Guid id)
    {
        var appointment = await sender.Send(new GetAppointmentByIdQuery(id));
        return appointment.ToAppointmentContract();
    }

    [HttpPost]
    public async Task<ActionResult<AppointmentContract>> Create(
        [FromBody] CreateAppointmentContract createAppointmentContract)
    {
        var appointment = await sender.Send(createAppointmentContract.ToCreateAppointmentCommand());
        return CreatedAtAction(nameof(GetById), new { id = appointment.Id }, appointment.ToAppointmentContract());
    }

    [HttpPost("{id:guid}:approve")]
    public async Task<ActionResult<AppointmentContract>> Approve([FromRoute] Guid id)
    {
        var appointment = await sender.Send(new ApproveAppointmentCommand(id));
        return NoContent();
    }

    [HttpPost("{id:guid}:cancel")]
    public async Task<ActionResult<AppointmentResult>> Cancel([FromRoute] Guid id)
    {
        await sender.Send(new CancelAppointmentCommand(id));
        return NoContent();
    }

    [HttpGet("{appointmentId:guid}/result")]
    public async Task<ActionResult<ResultContract>> GetAppointmentResultById([FromRoute] Guid appointmentId)
    {
        var result = await sender.Send(new GetResultByAppointmentIdQuery(appointmentId));
        return result.ToResultContract();
    }

    [HttpPatch("{appointmentId:guid}/result")]
    public async Task<ActionResult<ResultContract>> UpdateAppointmentResult([FromRoute] Guid appointmentId, [FromBody] UpdateAppointmentResultContract updateAppointmentResultContract)
    {
        var result = await sender.Send(updateAppointmentResultContract.ToUpdateAppointmentResultCommand(appointmentId));
        return CreatedAtAction(nameof(GetAppointmentResultById), new { appointmentId = result.AppointmentId }, result.ToResultContract());
    }
}