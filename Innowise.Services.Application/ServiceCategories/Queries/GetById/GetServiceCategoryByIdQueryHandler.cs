using Innowise.Services.Application.Extensions;
using Innowise.Services.Application.Interfaces;
using Innowise.Services.Application.ServiceCategories.Common;
using Innowise.Services.Application.ServiceCategories.Exceptions;
using MediatR;

namespace Innowise.Services.Application.ServiceCategories.Queries.GetById;

public class GetServiceCategoryByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetServiceCategoryByIdQuery, ServiceCategoryResult>
{
    public async Task<ServiceCategoryResult> Handle(GetServiceCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var serviceCategory = await unitOfWork.ServiceCategoryRepository.GetByIdAsync(request.ServiceCategoryId);
        if (serviceCategory is null)
        {
            throw new ServiceCategoryNotFoundException();
        }

        return serviceCategory.ToServiceCategoryResult();
    }
}