using Microsoft.Extensions.DependencyInjection;
using System.Net.Mail;
using System.Net;
using InnoClinic.Services.Email;
using Microsoft.Extensions.Configuration;
using InnoClinic.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using InnoClinic.Services.Endpoints;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace InnoClinic.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddEmail(this IServiceCollection services, IConfiguration configuration)
    {
        var emailOptions = new EmailOptions();
        configuration.Bind(EmailOptions.Email, emailOptions);

        services.Configure<EmailOptions>(configuration.GetSection(EmailOptions.Email));

        services.AddScoped(x => new SmtpClient()
        {
            Host = emailOptions.Host,
            Port = emailOptions.Port,
            Credentials = new NetworkCredential(emailOptions.Username, emailOptions.Password),
            EnableSsl = true,
        });

        services.AddScoped<IEmailService, EmailService>();

        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptions = new JwtOptions();
        configuration.Bind(JwtOptions.Jwt, jwtOptions);

        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.Jwt));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = jwtOptions.ValidIssuer,
                ValidAudience = jwtOptions.ValidAudience,
                IssuerSigningKey = jwtOptions.GetSymmetricSecurityKey(),
                ValidateIssuer = jwtOptions.ValidateIssuer,
                ValidateAudience = jwtOptions.ValidateAudience,
                ValidateIssuerSigningKey = jwtOptions.ValidateIssuerSigningKey,
                ValidateLifetime = jwtOptions.ValidateLifeTime
            };
        });

        services.AddAuthorization();

        return services;
    }

    public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
    {
        var endpointServiceDescriptors = assembly
            .DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                           type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();

        services.TryAddEnumerable(endpointServiceDescriptors);

        return services;
    }
}