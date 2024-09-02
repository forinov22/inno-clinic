using Microsoft.EntityFrameworkCore;
using Profiles.Application.Interfaces.Repositories;
using Profiles.Domain.Entities;

namespace Profiles.Infrastructure.Persistence.Repositories;

internal class AccountRepository(ProfilesDbContext context) : IAccountRepository
{
    public async Task<Account?> GetByExternalIdAsync(Guid externalId)
    {
        return await context.Accounts.FirstOrDefaultAsync(account => account.ExternalId == externalId);
    }

    public void Add(Account account)
    {
        context.Accounts.Add(account);
    }

    public async Task SaveAllAsync()
    {
        await context.SaveChangesAsync();
    }
}