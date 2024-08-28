using Innowise.Services.Application.Extensions;
using Innowise.Services.Application.Interfaces;
using Innowise.Services.Application.ServiceCategories.Exceptions;
using Innowise.Services.Application.Services.Common;
using Innowise.Services.Application.Services.Exceptions;
using Innowise.Services.Application.Specializations.Exceptions;
using MediatR;

namespace Innowise.Services.Application.Services.Commands.Edit;

public class EditServiceCommandHandler(
    IUnitOfWork unitOfWork) : IRequestHandler<EditServiceCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(EditServiceCommand request, CancellationToken cancellationToken)
    {
        var serviceTask = unitOfWork.ServiceRepository.GetByIdAsync(request.ServiceId);
        var serviceCategoryTask = unitOfWork.ServiceCategoryRepository.GetByIdAsync(request.ServiceCategoryId);
        var specializationTask = unitOfWork.SpecializationRepository.GetByIdAsync(request.SpecializationId);

        await Task.WhenAll(serviceTask, serviceCategoryTask, specializationTask);

        var service = serviceTask.Result;
        if (service is null)
        {
            throw new ServiceNotFoundException();
        }

        var serviceCategory = serviceCategoryTask.Result;
        if (serviceCategory is null)
        {
            throw new ServiceCategoryNotFoundException();
        }

        var specialization = specializationTask.Result;
        if (specialization is null)
        {
            throw new SpecializationNotFoundException();
        }

        service.ServiceName = request.ServiceName;
        service.IsActive = request.IsActive;
        service.Price = request.Price;
        service.ServiceCategory = serviceCategory;
        service.Specialization = specialization;

        unitOfWork.ServiceRepository.Update(service);
        await unitOfWork.SaveAllAsync();

        return service.ToServiceResult();
    }
}