using Innowise.Services.Application.Services.Queries.GetAll;
using Innowise.Services.Application.Services.Queries.GetById;
using Innowise.Services.Contracts.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.API.Extensions;

namespace Innowise.Services.ApiHost.Controllers;

[ApiController]
[Route("api/services")]
public class ServiceController(ISender mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServiceResponse>>> List()
    {
        var query = new GetAllServicesQuery();
        var result = await mediator.Send(query);
        return result.Select(service => service.ToServiceResponse()).ToList();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ServiceResponse>> GetById(Guid id)
    {
        var query = new GetServiceByIdQuery(id);
        var result = await mediator.Send(query);
        return result.ToServiceResponse();
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse>> Create([FromBody] CreateServiceRequest request)
    {
        var result = await mediator.Send(request.ToCreateServiceCommand());
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result.ToServiceResponse());
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ServiceResponse>> Edit(Guid id, [FromBody] EditServiceRequest request)
    {
        var result = await mediator.Send(request.ToEditServiceCommand(id));
        return result.ToServiceResponse();
    }
}