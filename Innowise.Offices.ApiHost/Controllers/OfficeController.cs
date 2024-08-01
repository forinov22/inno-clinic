using Innowise.Offices.ApiHost.Extensions;
using Innowise.Offices.Application.Offices.Queries.GetAll;
using Innowise.Offices.Application.Offices.Queries.GetById;
using Innowise.Offices.Contracts.Offices;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Innowise.Offices.ApiHost.Controllers;

[ApiController]
[Route("api/offices")]
public class OfficeController(ISender mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OfficeResponse>>> List()
    {
        var offices = await mediator.Send(new GetAllOfficesQuery());
        return offices.Select(office => office.ToOfficeResponse()).ToList();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OfficeResponse>> GetById(Guid id)
    {
        var office = await mediator.Send(new GetOfficeByIdQuery(id));
        return office.ToOfficeResponse();
    }

    [HttpPost]
    public async Task<ActionResult<OfficeResponse>> Create([FromForm] CreateOfficeRequest createOffice)
    {
        var office = await mediator.Send(createOffice.ToCreateOfficeCommand());
        return CreatedAtAction(nameof(GetById), new { id = office.Id }, office.ToOfficeResponse());
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<OfficeResponse>> Edit([FromRoute] Guid id, [FromBody] EditOfficeRequest editOffice)
    {
        var office = await mediator.Send(editOffice.ToEditOfficeCommand(id));
        return office.ToOfficeResponse();
    }
}