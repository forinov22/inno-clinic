using System.ComponentModel.DataAnnotations;

namespace Innowise.Common.Services.Email;

public class EmailOptions
{
    public const string Email = "EmailOptions";
    [Required]
    public string Host { get; set; } = string.Empty;
    [Required]
    public int Port { get; set; }
    [Required]
    public string Username { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}