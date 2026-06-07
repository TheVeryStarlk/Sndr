using Microsoft.Extensions.DependencyInjection;
using Sndr.Client;

var collection = new ServiceCollection();

collection.AddSndrClient(options => options.Key = Environment.GetEnvironmentVariable("Key", EnvironmentVariableTarget.User));

var services = collection.BuildServiceProvider();
var client = services.GetRequiredService<SndrClient>();

return;