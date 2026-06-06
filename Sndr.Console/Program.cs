using Microsoft.Extensions.DependencyInjection;
using Sndr.Client;

var services = new ServiceCollection()
    .AddSndrClient(options => options.Key = Environment.GetEnvironmentVariable("Key"))
    .BuildServiceProvider();

var client = services.GetRequiredService<SndrClient>();

