using Profiles.Domain.Entities;

namespace Profiles.Application.Interfaces.Repositories;

public interface ISpecializationRepository
{
    Task<Specialization?> GetSpecializationByIdAsync(Guid specializationId);
    Task FetchSpecializationsAsync();
}