using Microsoft.Extensions.DependencyInjection;

namespace Sndr.Client;

public static class SndrClientServiceCollectionExtensions
{
    public static IServiceCollection AddSndr(this IServiceCollection services, Action<SndrClientOptions> configure)
    {
        services.Configure(configure);

        services.AddHttpClient(nameof(SndrClient));

        services.AddTransient<SndrClient>();

        return services;
    }
}