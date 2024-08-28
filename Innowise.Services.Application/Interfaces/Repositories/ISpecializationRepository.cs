using Services.Domain;

namespace Innowise.Services.Application.Interfaces.Repositories;

public interface ISpecializationRepository
{
    Task<IEnumerable<Specialization>> GetAllAsync();
    Task<Specialization?> GetByIdAsync(Guid specializationId);
    void Add(Specialization specialization);
    void Update(Specialization specialization);
    Task SaveAllAsync();
}