namespace Innowise.Services.Application.Services.Common;

public record ServiceResult(
    Guid Id,
    string ServiceName,
    decimal Price,
    bool IsActive,
    Guid SpecializationId,
    string SpecializationName,
    Guid ServiceCategoryId,
    string ServiceCategoryName,
    int TimeSlotSize);