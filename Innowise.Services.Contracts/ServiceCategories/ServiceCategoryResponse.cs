using System.Text.Json.Serialization;

namespace Innowise.Services.Contracts.ServiceCategories;

public record ServiceCategoryResponse(
    [property:JsonPropertyName("id")]
    Guid Id,
    [property:JsonPropertyName("categoryName")]
    string CategoryName,
    [property:JsonPropertyName("timeSlotSize")]
    int TimeSlotSize);