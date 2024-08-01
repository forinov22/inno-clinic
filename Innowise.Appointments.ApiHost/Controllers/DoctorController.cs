using Appointments.Application.Appointments.Common;
using Appointments.Application.Doctors.Common;
using Appointments.Application.Doctors.Queries.GetFreeForDoctor;
using Appointments.Application.Doctors.Queries.GetUpcoming;
using Innowise.Appointments.Contracts.Appointments;
using Innowise.Appointments.ApiHost.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Appointments.API.Controllers;

[ApiController]
[Route("api/doctors")]
public class DoctorController(ISender sender)
{
    [HttpGet("{id:guid}/appointments")]
    public async Task<ActionResult<IEnumerable<AppointmentResponse>>> ListUpcoming([FromRoute] Guid id)
    {
        var appointments = await sender.Send(new GetDoctorUpcomingAppointmentsQuery(id));
        return appointments.Select(appointment => appointment.ToAppointmentResponse()).ToList();
    }

    [HttpGet("{id:guid}/schedule")]
    public async Task<ActionResult<IEnumerable<TimeSlotResult>>> GetFreeTimeSlotsForDoctor([FromRoute] Guid id,
                                                                                           [FromQuery] DateTime? date)
    {
        var timeSlots = await sender.Send(new GetFreeTimeSlotsForDoctorQuery(id, date));
        return timeSlots.ToList();
    }
}