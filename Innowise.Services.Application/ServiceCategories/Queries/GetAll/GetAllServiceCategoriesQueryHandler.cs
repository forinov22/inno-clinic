using Innowise.Services.Application.Extensions;
using Innowise.Services.Application.Interfaces;
using Innowise.Services.Application.ServiceCategories.Common;
using MediatR;

namespace Innowise.Services.Application.ServiceCategories.Queries.GetAll;

public class GetAllServiceCategoriesQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllServiceCategoriesQuery, IEnumerable<ServiceCategoryResult>>
{
    public async Task<IEnumerable<ServiceCategoryResult>> Handle(GetAllServiceCategoriesQuery request, CancellationToken cancellationToken)
    {
        var serviceCategories = await unitOfWork.ServiceCategoryRepository.GetAllAsync();
        return serviceCategories.Select(serviceCategory => serviceCategory.ToServiceCategoryResult()).ToList();
    }
}