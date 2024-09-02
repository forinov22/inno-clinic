using Auth.Application.Interfaces.Repositories;
using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Persistence.Repositories;

public class AccountRepository(AuthDbContext context) : IAccountRepository
{
    public async Task<Account?> GetByIdAsync(Guid accountId)
    {
        return await context.Accounts.FirstOrDefaultAsync(account => account.AccountId == accountId);
    }

    public async Task<Account?> GetByEmailAsync(string email)
    {
        return await context.Accounts.FirstOrDefaultAsync(account => account.Email == email);
    }

    public async Task<Account?> GetByActivationLinkAsync(string activationLink)
    {
        return await context.Accounts.FirstOrDefaultAsync(account => account.ActivationLink == activationLink);
    }

    public async Task<bool> IsEmailUniqueAsync(string email)
    {
        return !await context.Accounts.AnyAsync(account => account.Email == email);
    }

    public void Create(Account account)
    {
        context.Accounts.Add(account);
    }

    public async Task SaveAllAsync()
    {
        await context.SaveChangesAsync();
    }
}