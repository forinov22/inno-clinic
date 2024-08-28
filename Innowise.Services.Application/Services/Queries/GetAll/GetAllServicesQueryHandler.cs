using Innowise.Services.Application.Extensions;
using Innowise.Services.Application.Interfaces;
using Innowise.Services.Application.Services.Common;
using MediatR;

namespace Innowise.Services.Application.Services.Queries.GetAll;

public class GetAllServicesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllServicesQuery, IEnumerable<ServiceResult>>
{
    public async Task<IEnumerable<ServiceResult>> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
    {
        var services = await unitOfWork.ServiceRepository.GetAllAsync();
        return services.Select(service => service.ToServiceResult()).ToList();
    }
}