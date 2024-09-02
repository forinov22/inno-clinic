using Profiles.Domain.Entities;

namespace Profiles.Application.Interfaces.Repositories;

public interface IAccountRepository
{
    Task<Account?> GetByExternalIdAsync(Guid externalId);
    void Add(Account account);
}