using Innowise.Profiles.Contracts.Patients;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Profiles.API.Extensions;
using Profiles.Application.Patients.Commands.LinkToAccount;
using Profiles.Application.Patients.Queries.GetById;

namespace Profiles.API.Controllers;

[Route("api/patients")]
[ApiController]
public class PatientController(ISender sender) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PatientResponse>> GetById([FromRoute] Guid id)
    {
        var patientProfile = await sender.Send(new GetPatientProfileByIdQuery(id));
        return patientProfile.ToPatientResponse();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePatientRequest createPatientRequest)
    {
        var patientProfile = await sender.Send(createPatientRequest.ToCreatePatientProfileCommand());
        return CreatedAtAction(nameof(GetById), new { patientId = patientProfile.Id }, patientProfile.ToPatientResponse());
    }

    [HttpGet("profile-matches")]
    public async Task<ActionResult<IEnumerable<PatientResponse>>> FindPatientProfileMatches([FromQuery] ProfileMatchesRequest profileMatchesRequest)
    {
        var matches = await sender.Send(profileMatchesRequest.ToFindPatientProfileMatchesQuery());
        return matches.Select(match => match.ToPatientResponse()).ToList();
    }

    [HttpPost("{patientId:guid}:link-to-account/{accountId:guid}")]
    public async Task<IActionResult> LinkPatientProfileToAccount([FromRoute] Guid patientId, [FromRoute] Guid accountId)
    {
        await sender.Send(new LinkPatientProfileToAccountCommand(patientId, accountId));
        return NoContent();
    }
}