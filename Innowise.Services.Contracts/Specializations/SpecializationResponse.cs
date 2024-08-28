using System.Text.Json.Serialization;

namespace Innowise.Services.Contracts.Specializations;

public record SpecializationResponse(
    [property:JsonPropertyName("id")]
    Guid Id,
    [property:JsonPropertyName("specializationName")]
    string SpecializationName,
    [property:JsonPropertyName("isActive")]
    bool IsActive);