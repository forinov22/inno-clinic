using Innowise.Services.Application.ServiceCategories.Queries.GetAll;
using Innowise.Services.Application.ServiceCategories.Queries.GetById;
using Innowise.Services.Contracts.ServiceCategories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.API.Extensions;

namespace Innowise.Services.ApiHost.Controllers;

[ApiController]
[Route("api/service-categories")]
public class ServiceCategoriesController(ISender mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServiceCategoryResponse>>> List()
    {
        var query = new GetAllServiceCategoriesQuery();
        var result = await mediator.Send(query);
        return result.Select(serviceCategory => serviceCategory.ToServiceCategoryResponse()).ToList();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ServiceCategoryResponse>> GetById(Guid id)
    {
        var query = new GetServiceCategoryByIdQuery(id);
        var result = await mediator.Send(query);
        return result.ToServiceCategoryResponse();
    }

    [HttpPost]
    public async Task<ActionResult<ServiceCategoryResponse>> Create([FromBody] CreateServiceCategoryRequest request)
    {
        var result = await mediator.Send(request.ToCreateServiceCategoryCommand());
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result.ToServiceCategoryResponse());
    }
}