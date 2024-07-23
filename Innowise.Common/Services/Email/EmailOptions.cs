namespace Innowise.Common.Services.Email;

public class EmailOptions
{
    public const string Email = "EmailOptions";
    public string Host { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}