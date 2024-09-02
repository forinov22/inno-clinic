namespace Auth.Domain.Entities;

public class Token
{
    public Guid AccountId { get; set; }
    public Account Account { get; set; }
    public string RefreshToken { get; set; }
}