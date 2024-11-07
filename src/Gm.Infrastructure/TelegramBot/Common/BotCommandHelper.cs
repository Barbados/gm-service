using Gm.Infrastructure.TelegramBot.Model;
using Telegram.Bot.Types;

namespace Gm.Infrastructure.TelegramBot.Common;

public abstract class BotCommandHelper
{
    public static IEnumerable<BotCommand> GetAllCommands()
    {
        return new List<BotCommand>
        {
            new()
            {
                Command = BotCommandType.Start,
                Description = "начать общение"
            },
            new()
            {
                Command = BotCommandType.Subscribe,
                Description = "подписаться"
            },
            new()
            {
                Command = BotCommandType.Unsubscribe,
                Description = "отписаться"
            },
            new()
            {
                Command = BotCommandType.Test,
                Description = "отправить тестовое сообщение"
            }
        };
    }

    public static IEnumerable<BotCommand> GetAdminCommands()
    {
        return new List<BotCommand>
        {
            new()
            {
                Command = BotCommandType.AddPost,
                Description = "add text for new post"
            }
        };
    }
}