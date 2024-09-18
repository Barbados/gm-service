﻿using Gm.Infrastructure.TelegramBot.Model;
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
                Description = "to start conversation"
            }
        };
    }
}