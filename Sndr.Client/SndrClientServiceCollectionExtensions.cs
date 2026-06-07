using Microsoft.Extensions.DependencyInjection;

namespace Sndr.Client;

public static class SndrClientServiceCollectionExtensions
{
    public static IServiceCollection AddSndrClient(this IServiceCollection services, Action<SndrClientOptions> configure)
    {
        // Probably should validate the options too.
        services.Configure(configure);

        services.AddHttpClient(nameof(SndrClient));
        services.AddTransient<SndrClient>();

        return services;
    }
}