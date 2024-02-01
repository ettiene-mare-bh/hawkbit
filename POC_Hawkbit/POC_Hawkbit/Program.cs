using Microsoft.Extensions.DependencyInjection;

WriteLine("POC - Hawkbit");

var builder = Host.CreateDefaultBuilder(args);
builder.ConfigureServices(s => s.AddHostedService<SoftwareUpdateWorker>());

var host = builder.Build();
await host.RunAsync();