using Gm.Infrastructure.TelegramBot;
using Gm.Infrastructure.TelegramBot.Abstract;
using Gm.Infrastructure.TelegramBot.Services.Command;
using Gm.Infrastructure.TelegramBot.Services.Core;
using Gm.Infrastructure.TelegramBot.Services.Scheduler;
using Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace Gm.Infrastructure.Configuration.DependencyInjection;

public static class TelegramBotExtensions
{
    public static void ConfigureTelegramBot(this IServiceCollection services, IConfiguration configuration)
    {
        var telegramSettings = configuration
            .GetSection(BotSettings.TgBot)
            .Get<BotSettings>();
        ArgumentNullException.ThrowIfNull(telegramSettings);
        
        const string httpBotClientName = "telegram_bot_client";
        services.AddHttpClient(httpBotClientName).RemoveAllLoggers()
            .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
            {
                var botOptions = new TelegramBotClientOptions(telegramSettings.Token);

                return new TelegramBotClient(botOptions, httpClient);
            });

        services.AddScoped<ISenderService, SenderService>();
        services.AddScoped<IUpdateHandler, UpdateHandler>();
        services.AddScoped<IReceiverService, ReceiverService>();
        services.AddScoped<IBotCommandService, BotCommandService>();

        services.AddHostedService<SchedulerService>();
    }
}