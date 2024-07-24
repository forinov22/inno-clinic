using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Innowise.Common.Services.Authentication;

public class JwtOptions
{
    public const string Jwt = "JwtOptions";
    [Required]
    public string ValidIssuer { get; set; } = string.Empty;
    [Required]
    public string ValidAudience { get; set; } = string.Empty;
    [Required]
    [MinLength(64)]
    public string SecurityKey { get; set; } = string.Empty;
    [Required]
    public bool ValidateIssuer { get; set; }
    [Required]
    public bool ValidateAudience { get; set; }
    [Required]
    public bool ValidateIssuerSigningKey { get; set; }
    [Required]
    public bool ValidateLifeTime { get; set; }

    public SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));
    }
}