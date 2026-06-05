using Microsoft.Extensions.DependencyInjection;

namespace Sndr.Client;

public static class SndrClientServiceCollectionExtensions
{
    public static IServiceCollection AddSndr(this IServiceCollection services)
    {
        services.AddHttpClient(nameof(SndrClient));

        return services;
    }
}