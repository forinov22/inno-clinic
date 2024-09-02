using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Auth.Application.Interfaces;
using Auth.Domain.Entities;
using InnoClinic.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Infrastructure.Services;

internal class TokenGenerator(IOptions<JwtOptions> options) : ITokenGenerator
{
    private readonly JwtOptions _jwtOptions = options.Value;

    public string GenerateToken(Account account, int lifeTimeInDays)
    {
        List<Claim> claims = [
            new Claim(JwtRegisteredClaimNames.Email, account.Email),
            new Claim("role", account.Role.ToString())
        ];

        var jwt = new JwtSecurityToken(
            issuer: _jwtOptions.ValidIssuer,
            audience: _jwtOptions.ValidAudience,
            claims: claims,
            expires: DateTime.Now.Add(TimeSpan.FromDays(lifeTimeInDays)),
            signingCredentials: new SigningCredentials(_jwtOptions.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}