using Gm.Infrastructure.Configuration.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder();
var services = builder.Services;
var configuration = builder.Configuration;

// Configure dependencies
services.ConfigureLogging(configuration);
services.ConfigureDatabase(configuration);
services.ConfigureMediatr();
services.ConfigureTelegramBot(configuration);
services.ConfigureScheduler(configuration);

var host = builder.Build();

// Initialize db in case of real deployment
if (!builder.Environment.IsDevelopment())
    host.InitializeDatabase();

await host.RunAsync();