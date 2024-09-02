using Profiles.Domain.Entities;

namespace Profiles.Application.Interfaces.Repositories;

public interface IOfficeRepository
{
    Task<Office?> GetOfficeByIdAsync(Guid officeId);
    Task FetchOfficesAsync();
}