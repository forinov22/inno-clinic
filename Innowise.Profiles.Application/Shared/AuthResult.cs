namespace Profiles.Application.Shared;

public record AuthResult(AccountResult Account, string AccessToken, string RefreshToken)
{
    public AccountResult Account { get; set; } = Account;
    public string AccessToken { get; set; } = AccessToken;
    public string RefreshToken { get; set; } = RefreshToken;
}


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
}
