using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace InnoClinic.Authentication;

public class JwtOptions
{
    public const string Jwt = "JwtOptions";
    public string ValidIssuer { get; set; }
    public string ValidAudience { get; set; }
    public string SecurityKey { get; set; }
    public bool ValidateIssuer { get; set; }
    public bool ValidateAudience { get; set; }
    public bool ValidateIssuerSigningKey { get; set; }
    public bool ValidateLifeTime { get; set; }

    public SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));
    }
}