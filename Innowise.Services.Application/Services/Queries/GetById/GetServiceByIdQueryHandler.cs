using Innowise.Services.Application.Extensions;
using Innowise.Services.Application.Interfaces;
using Innowise.Services.Application.Services.Common;
using Innowise.Services.Application.Services.Exceptions;
using MediatR;

namespace Innowise.Services.Application.Services.Queries.GetById;

public class GetServiceByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetServiceByIdQuery, ServiceResult>
{
    public async Task<ServiceResult> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
    {
        var service = await unitOfWork.ServiceRepository.GetByIdAsync(request.ServiceId);
        if (service is null)
        {
            throw new ServiceNotFoundException();
        }

        return service.ToServiceResult();
    }
}