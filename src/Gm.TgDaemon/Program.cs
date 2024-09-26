﻿using Gm.Infrastructure.Configuration.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder();
var services = builder.Services;
var configuration = builder.Configuration;

services.ConfigureDatabase(configuration);
services.ConfigureMediatr();
services.ConfigureTelegramBot(configuration);
services.ConfigureScheduler(configuration);

var host = builder.Build();
host.InitializeDatabase();

await host.RunAsync();