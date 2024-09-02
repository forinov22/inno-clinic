using Auth.Application.Interfaces;
using Auth.Application.Interfaces.Repositories;
using Auth.Infrastructure.Persistence.Repositories;

namespace Auth.Infrastructure.Persistence;

public class UnitOfWork(AuthDbContext context) : IUnitOfWork
{
    private readonly Lazy<IAccountRepository> _accountRepository = new(() => new AccountRepository(context));
    private readonly Lazy<ITokenRepository> _tokenRepository = new(() => new TokenRepository(context));

    public IAccountRepository AccountRepository => _accountRepository.Value;
    public ITokenRepository TokenRepository => _tokenRepository.Value;

    public async Task SaveAllAsync()
    {
        await context.SaveChangesAsync();
    }
}