using Innowise.Services.Application.ServiceCategories.Common;
using Innowise.Services.Application.Services.Common;
using Innowise.Services.Application.Specializations.Common;
using Services.Domain;

namespace Innowise.Services.Application.Extensions;

public static class MappingExtensions
{
    public static ServiceResult ToServiceResult(this Service service)
    {
        return new ServiceResult(service.Id, service.ServiceName, service.Price, service.IsActive,
            service.SpecializationId, service.Specialization.SpecializationName,
            service.ServiceCategoryId, service.ServiceCategory.CategoryName,
            service.ServiceCategory.TimeSlotSize);
    }

    public static ServiceCategoryResult ToServiceCategoryResult(this ServiceCategory serviceCategory)
    {
        return new ServiceCategoryResult(serviceCategory.Id, serviceCategory.CategoryName,
            serviceCategory.TimeSlotSize);
    }

    public static SpecializationResult ToSpecializationResult(this Specialization specialization)
    {
        return new SpecializationResult(specialization.Id, specialization.SpecializationName, specialization.IsActive);
    }
}