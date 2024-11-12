using System.Globalization;
using Gm.Infrastructure.Configuration.DependencyInjection;
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

// TestDateCultureAsync();

await host.RunAsync();

void TestDateCultureAsync()
{
    Console.WriteLine(ComposeGmMessage());
}

string ComposeGmMessage()
{
    var culture = new CultureInfo("ru-RU");
    var message = string.Create(culture, $"Доброе утро!\nСегодня {DateTime.Today:D}");
    message += $"\nХорошего дня! Увидимся завтра.";

    return message;
}