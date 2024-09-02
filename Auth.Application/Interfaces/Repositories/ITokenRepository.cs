using Auth.Domain.Entities;

namespace Auth.Application.Interfaces.Repositories;

public interface ITokenRepository
{
    Task<Account?> GetAccountByRefreshTokenAsync(string refreshToken);
}