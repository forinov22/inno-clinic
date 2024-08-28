namespace Innowise.Services.Contracts.Services;

public record CreateServiceRequest(
    string ServiceName,
    decimal Price,
    bool IsActive,
    Guid ServiceCategoryId,
    Guid SpecializationId);