using Innowise.Profiles.Contracts.Receptionists;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Profiles.API.Extensions;
using Profiles.Application.Receptionists.Queries.GetById;

namespace Profiles.API.Controllers;

[Route("api/receptionists")]
[ApiController]
public class ReceptionistController(ISender sender) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ReceptionistResponse>> GetById([FromRoute] Guid id)
    {
        var receptionistProfile = await sender.Send(new GetReceptionistProfileByIdQuery(id));
        return receptionistProfile.ToReceptionistResponse();
    }

    [HttpPost]
    public async Task<ActionResult<ReceptionistResponse>> Create([FromBody] CreateReceptionistRequest createReceptionistRequest)
    {
        var receptionistProfile = await sender.Send(createReceptionistRequest.ToCreateReceptionistProfileCommand());
        return CreatedAtAction(nameof(GetById), new { receptionistId = receptionistProfile.Id }, receptionistProfile.ToReceptionistResponse());
    }
}