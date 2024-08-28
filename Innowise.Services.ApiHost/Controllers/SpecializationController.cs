using Innowise.Services.Application.Specializations.Queries.GetAll;
using Innowise.Services.Application.Specializations.Queries.GetById;
using Innowise.Services.Contracts.Specializations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.API.Extensions;

namespace Innowise.Services.ApiHost.Controllers;

[ApiController]
[Route("api/specializations")]
public class SpecializationController(ISender mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SpecializationResponse>>> List()
    {
        var query = new GetAllSpecializationsQuery();
        var result = await mediator.Send(query);
        return result.Select(specialization => specialization.ToSpecializationResponse()).ToList();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SpecializationResponse>> GetById(Guid id)
    {
        var query = new GetSpecializationByIdQuery(id);
        var result = await mediator.Send(query);
        return result.ToSpecializationResponse();
    }

    [HttpPost]
    public async Task<ActionResult<SpecializationResponse>> Create(
        [FromBody] CreateSpecializationRequest request)
    {
        var result = await mediator.Send(request.ToCreateSpecializationCommand());
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result.ToSpecializationResponse());
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<SpecializationResponse>> Edit(
        Guid id, [FromBody] EditSpecializationRequest request)
    {
        var result = await mediator.Send(request.ToEditSpecializationCommand(id));
        return result.ToSpecializationResponse();
    }
}