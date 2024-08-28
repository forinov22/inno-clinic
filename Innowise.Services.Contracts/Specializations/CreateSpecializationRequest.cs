namespace Innowise.Services.Contracts.Specializations;

public record CreateSpecializationRequest(
    string SpecializationName,
    bool IsActive);