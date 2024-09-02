using Auth.Domain.Entities;

namespace Auth.Application.Interfaces.Repositories;

public interface IAccountRepository
{
    Task<Account?> GetByIdAsync(Guid accountId);
    Task<Account?> GetByEmailAsync(string email);
    Task<Account?> GetByActivationLinkAsync(string activationLink);
    Task<bool> IsEmailUniqueAsync(string  email);
    void Create(Account account);
}