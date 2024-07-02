using Appointments.Application.Appointments.Commands.Approve;
using Appointments.Application.Appointments.Commands.Cancel;
using Appointments.Application.Appointments.Commands.Create;
using Appointments.Application.Appointments.Common;
using Appointments.Application.Appointments.Queries.GetAllUpcoming;
using Appointments.Application.Appointments.Queries.GetById;
using Appointments.Application.Appointments.Queries.GetDoctorUpcoming;
using Appointments.Application.Appointments.Queries.GetPatientHistory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Appointments.API.Controllers;

[Route("api/appointments")]
[ApiController]
public class AppointmentController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppointmentResult>>> GetAllUpcoming()
    {
        var appointments = await sender.Send(new GetAllUpcomingAppointmentsQuery());
        return appointments.ToList();
    }

    [HttpGet("{appointmentId:guid}")]
    public async Task<ActionResult<AppointmentResult>> GetById([FromRoute] Guid appointmentId)
    {
        var appointment = await sender.Send(new GetAppointmentByIdQuery(appointmentId));
        return appointment;
    }

    [HttpGet("doctor/{doctorId:guid}")]
    public async Task<ActionResult<IEnumerable<AppointmentResult>>> GetDoctorUpcoming([FromRoute] Guid doctorId)
    {
        var appointments = await sender.Send(new GetDoctorUpcomingAppointmentsQuery(doctorId));
        return appointments.ToList();
    }

    [HttpGet("patient/{patientId:guid}")]
    public async Task<ActionResult<IEnumerable<AppointmentResult>>> GetPatientHistory([FromRoute] Guid patientId)
    {
        var appointments = await sender.Send(new GetPatientAppointmentsHistoryQuery(patientId));
        return appointments.ToList();
    }

    [HttpPost]
    public async Task<ActionResult<AppointmentResult>> Create(
        [FromBody] CreateAppointmentCommand createAppointmentCommand)
    {
        var appointmentResult = await sender.Send(createAppointmentCommand);
        // todo: return 201 created at
        return appointmentResult;
    }

    [HttpPost("{appointmentId:guid}/approve")]
    public async Task<ActionResult<AppointmentResult>> Approve([FromRoute] Guid appointmentId)
    {
        var appointmentResult = await sender.Send(new ApproveAppointmentCommand(appointmentId));
        return CreatedAtAction(nameof(GetById), new {appointmentId}, appointmentResult);
    }

    [HttpPost("{appointmentId:guid}/cancel")]
    public async Task<ActionResult<AppointmentResult>> Cancel([FromRoute] Guid appointmentId)
    {
        await sender.Send(new CancelAppointmentCommand(appointmentId));
        return NoContent();
    }
}   