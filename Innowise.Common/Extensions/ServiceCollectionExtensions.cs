using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Innowise.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static OptionsBuilder<T> BindOptions<T>(this IServiceCollection services,
        Action<T> configureOptions) where T : class
    {
        return services
               .AddOptions<T>()
               .Configure(configureOptions)
               .ValidateDataAnnotations()
               .ValidateOnStart();
    }
}