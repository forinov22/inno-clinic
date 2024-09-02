namespace Auth.Application.Accounts.Common;

public record AuthResult(AccountResult Account, string AccessToken, string RefreshToken)
{
    public AccountResult Account { get; set; } = Account;
    public string AccessToken { get; set; } = AccessToken;
    public string RefreshToken { get; set; } = RefreshToken;
}