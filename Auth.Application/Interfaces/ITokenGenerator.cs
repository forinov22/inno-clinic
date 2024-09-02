using Auth.Domain.Entities;

namespace Auth.Application.Interfaces;

public interface ITokenGenerator
{
    string GenerateToken(Account  account, int lifeTimeInDays);
}