using Innowise.Profiles.Contracts.Doctors;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Profiles.API.Extensions;
using Profiles.Application.Doctors.Queries.GetAllAtWork;
using Profiles.Application.Doctors.Queries.GetById;

namespace Profiles.API.Controllers;

[Route("api/doctors")]
[ApiController]
public class DoctorController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DoctorResponse>>> ListAtWork()
    {
        var doctors = await sender.Send(new GetAllDoctorsAtWorkQuery());
        return doctors.Select(doctor => doctor.ToDoctorResponse()).ToList();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<DoctorResponse>> GetById([FromRoute] Guid id)
    {
        var doctorProfile = await sender.Send(new GetDoctorProfileByIdQuery(id));
        return doctorProfile.ToDoctorResponse();
    }

    [HttpPost]
    public async Task<ActionResult<DoctorResponse>> Create([FromForm] CreateDoctorRequest createDoctorRequest)
    {
        var doctorProfile = await sender.Send(createDoctorRequest.ToCreateDoctorProfileCommand());
        return CreatedAtAction(nameof(GetById), new { doctorId = doctorProfile.Id }, doctorProfile.ToDoctorResponse());
    }
}