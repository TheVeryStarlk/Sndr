using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Sndr.Client;

public static class SndrClientServiceCollectionExtensions
{
    public static IServiceCollection AddSndrClient(this IServiceCollection services, Action<SndrClientOptions> configure)
    {
        // Probably should validate the options too?
        services.Configure(configure);

        services.AddHttpClient(
            nameof(SndrClient),
            static (services, client) =>
            {
                var options = services.GetRequiredService<IOptions<SndrClientOptions>>();

                // Think of a friendly way for API versioning. 
                client.BaseAddress = new Uri("https://api.sndr.sh/v1/");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", options.Value.Key);
            });

        services.AddTransient<SndrClient>();

        return services;
    }
}