namespace Innowise.Services.Contracts.Services;

public record EditServiceRequest(
    string ServiceName,
    decimal Price,
    bool IsActive,
    Guid ServiceCategoryId,
    Guid SpecializationId);