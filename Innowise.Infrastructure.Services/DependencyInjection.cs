using Microsoft.Extensions.DependencyInjection;
using System.Net.Mail;
using System.Net;
using InnoClinic.Services.Email;
using Microsoft.Extensions.Configuration;
using InnoClinic.Authentication;
using Innowise.Common.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;

namespace InnoClinic.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddEmail(this IServiceCollection services, Action<EmailOptions> configureOptions)
    {
        services.BindOptions(configureOptions);

        services.AddScoped(serviceProvider =>
        {
            var emailOptions = serviceProvider.GetRequiredService<IOptions<EmailOptions>>().Value;

            return new SmtpClient()
            {
                Host = emailOptions.Host,
                Port = emailOptions.Port,
                Credentials = new NetworkCredential(emailOptions.Username, emailOptions.Password),
                EnableSsl = true,
            };
        });

        services.AddScoped<IEmailService, EmailService>();

        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services, Action<JwtBearerOptions> configureOptions)
    {
        services.BindOptions(configureOptions);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(configureOptions);

        services.AddAuthorization();

        return services;
    }
}