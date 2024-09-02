using Auth.Application.Interfaces.Repositories;

namespace Auth.Application.Interfaces;

public interface IUnitOfWork
{
    IAccountRepository AccountRepository { get; }
    ITokenRepository TokenRepository { get; }
    Task SaveAllAsync();
}