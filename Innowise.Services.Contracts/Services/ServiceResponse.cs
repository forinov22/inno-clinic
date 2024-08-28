using System.Text.Json.Serialization;

namespace Innowise.Services.Contracts.Services;

public record ServiceResponse(
    [property:JsonPropertyName("id")]
    Guid Id,
    [property:JsonPropertyName("serviceName")]
    string ServiceName,
    [property:JsonPropertyName("price")]
    decimal Price,
    [property:JsonPropertyName("isActive")]
    bool IsActive,
    [property:JsonPropertyName("specializationId")]
    Guid SpecializationId,
    [property:JsonPropertyName("specializationName")]
    string SpecializationName,
    [property:JsonPropertyName("serviceCategoryId")]
    Guid ServiceCategoryId,
    [property:JsonPropertyName("serviceCategoryName")]
    string ServiceCategoryName,
    [property:JsonPropertyName("timeSlotSize")]
    int TimeSlotSize);