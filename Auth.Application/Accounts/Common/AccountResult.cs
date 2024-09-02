using Auth.Domain.Entities;

namespace Auth.Application.Accounts.Common;

public record AccountResult(
    Guid AccountId,
    string Email,
    string? PhoneNumber,
    string ActivationLink,
    bool IsEmailVerified,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    string? PhotoUrl,
    string Role)
{
    public Guid AccountId { get; set; } = AccountId;
    public string Email { get; set; } = Email;
    public string? PhoneNumber { get; set; } = PhoneNumber;
    public string ActivationLink { get; set; } = ActivationLink;
    public bool IsEmailVerified { get; set; } = IsEmailVerified;
    public DateTime CreatedAt { get; set; } = CreatedAt;
    public DateTime UpdatedAt { get; set; } = UpdatedAt;
    public string? PhotoUrl { get; set; } = PhotoUrl;
    public string Role { get; set; } = Role;

    public static AccountResult MapFromAccount(Account account)
    {
        return new AccountResult(
            account.AccountId,
            account.Email,
            account.PhoneNumber,
            account.ActivationLink,
            account.IsEmailVerified,
            account.CreatedAt,
            account.UpdatedAt,
            account.PhotoUrl,
            account.Role.ToString());
    }
}