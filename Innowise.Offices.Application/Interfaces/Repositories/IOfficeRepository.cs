using Innowise.Offices.Domain.Entities;

namespace Innowise.Offices.Application.Interfaces.Repositories;

public interface IOfficeRepository
{
    Task<IEnumerable<Office>> GetAllAsync();
    Task<Office?> GetByIdAsync(Guid officeId);
    void Add(Office office);
    void Update(Office office);
}