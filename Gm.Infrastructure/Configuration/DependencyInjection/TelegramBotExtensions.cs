using Gm.Infrastructure.TelegramBot;
using Gm.Infrastructure.TelegramBot.Abstract;
using Gm.Infrastructure.TelegramBot.Core;
using Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

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

        services.AddHostedService<SchedulerService>();
    }
}