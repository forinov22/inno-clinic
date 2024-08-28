using Innowise.Services.Application.ServiceCategories.Commands.Create;
using Innowise.Services.Application.ServiceCategories.Common;
using Innowise.Services.Application.Services.Commands.Create;
using Innowise.Services.Application.Services.Commands.Edit;
using Innowise.Services.Application.Services.Common;
using Innowise.Services.Application.Specializations.Commands.Create;
using Innowise.Services.Application.Specializations.Commands.Edit;
using Innowise.Services.Application.Specializations.Common;
using Innowise.Services.Contracts.ServiceCategories;
using Innowise.Services.Contracts.Services;
using Innowise.Services.Contracts.Specializations;

namespace Services.API.Extensions;

public static class MappingExtensions
{
    #region services

    public static ServiceResponse ToServiceResponse(this ServiceResult serviceResult)
    {
        return new ServiceResponse(serviceResult.Id, serviceResult.ServiceName, serviceResult.Price,
            serviceResult.IsActive, serviceResult.SpecializationId, serviceResult.SpecializationName,
            serviceResult.ServiceCategoryId, serviceResult.ServiceCategoryName, serviceResult.TimeSlotSize);
    }

    public static CreateServiceCommand ToCreateServiceCommand(this CreateServiceRequest createServiceRequest)
    {
        return new CreateServiceCommand(createServiceRequest.ServiceName, createServiceRequest.Price,
            createServiceRequest.IsActive, createServiceRequest.ServiceCategoryId,
            createServiceRequest.SpecializationId);
    }

    public static EditServiceCommand ToEditServiceCommand(this EditServiceRequest editServiceRequest,
        Guid serviceId)
    {
        return new EditServiceCommand(serviceId, editServiceRequest.ServiceName, editServiceRequest.Price,
            editServiceRequest.IsActive, editServiceRequest.ServiceCategoryId,
            editServiceRequest.SpecializationId);
    }

    #endregion

    #region specializations

    public static SpecializationResponse ToSpecializationResponse(
        this SpecializationResult specializationResult)
    {
        return new SpecializationResponse(specializationResult.Id, specializationResult.SpecializationName,
            specializationResult.IsActive);
    }

    public static CreateSpecializationCommand ToCreateSpecializationCommand(
        this CreateSpecializationRequest createSpecializationRequest)
    {
        return new CreateSpecializationCommand(createSpecializationRequest.SpecializationName,
            createSpecializationRequest.IsActive);
    }

    public static EditSpecializationCommand ToEditSpecializationCommand(
        this EditSpecializationRequest editSpecializationRequest, Guid specializationId)
    {
        return new EditSpecializationCommand(specializationId, editSpecializationRequest.SpecializationName,
            editSpecializationRequest.IsActive);
    }

    #endregion

    #region service categories

    public static ServiceCategoryResponse ToServiceCategoryResponse(
        this ServiceCategoryResult serviceCategoryResult)
    {
        return new ServiceCategoryResponse(serviceCategoryResult.Id, serviceCategoryResult.CategoryName,
            serviceCategoryResult.TimeSlotSize);
    }

    public static CreateServiceCategoryCommand ToCreateServiceCategoryCommand(
        this CreateServiceCategoryRequest createServiceCategoryRequest)
    {
        return new CreateServiceCategoryCommand(createServiceCategoryRequest.CategoryName,
            createServiceCategoryRequest.TimeSlotSize);
    }

    #endregion
}