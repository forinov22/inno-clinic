using Auth.Application.Interfaces.Repositories;
using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Persistence.Repositories;

public class TokenRepository(AuthDbContext context) : ITokenRepository
{
    public async Task<Account?> GetAccountByRefreshTokenAsync(string refreshToken)
    {
        var token = await context.Tokens.Include(token => token.Account)
            .FirstOrDefaultAsync(token => string.Equals(token.RefreshToken, refreshToken));

        return token?.Account;
    }
}