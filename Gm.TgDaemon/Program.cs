using Gm.Infrastructure.Configuration.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder();
var services = builder.Services;
var configuration = builder.Configuration;

services.ConfigureTelegramBot(configuration);
services.ConfigureScheduler(configuration);

var host = builder.Build();

await host.RunAsync();