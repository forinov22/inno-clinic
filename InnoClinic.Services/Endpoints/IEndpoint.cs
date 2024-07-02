using Microsoft.AspNetCore.Routing;

namespace InnoClinic.Services.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}