namespace Appointments.Application.Interfaces.HttpClients.Services;

public record ServiceResponse(
    Guid Id,
    string ServiceName,
    decimal Price,
    bool IsActive,
    Guid SpecializationId,
    string SpecializationName,
    Guid ServiceCategoryId,
    string ServiceCategoryName,
    int TimeSlotSize);