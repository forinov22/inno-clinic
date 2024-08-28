namespace Innowise.Services.Contracts.Specializations;

public record EditSpecializationRequest(
    string SpecializationName,
    bool IsActive);