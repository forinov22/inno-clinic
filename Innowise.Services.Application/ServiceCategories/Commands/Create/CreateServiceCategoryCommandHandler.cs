using Innowise.Services.Application.Extensions;
using Innowise.Services.Application.Interfaces;
using Innowise.Services.Application.ServiceCategories.Common;
using MediatR;
using Services.Domain;

namespace Innowise.Services.Application.ServiceCategories.Commands.Create;

public class CreateServiceCategoryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateServiceCategoryCommand, ServiceCategoryResult>
{
    public async Task<ServiceCategoryResult> Handle(CreateServiceCategoryCommand request, CancellationToken cancellationToken)
    {
        var serviceCategory = new ServiceCategory()
        {
            CategoryName = request.CategoryName,
            TimeSlotSize = request.TimeSlotSize
        };

        unitOfWork.ServiceCategoryRepository.Add(serviceCategory);
        await unitOfWork.SaveAllAsync();

        return serviceCategory.ToServiceCategoryResult();
    }
}