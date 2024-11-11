using Gm.Infrastructure.TelegramBot.Common;
using Gm.Infrastructure.TelegramBot.Model;

namespace Gm.Infrastructure.TelegramBot.Abstract;

public interface IConversationHelper
{
    BotCommandType SetCurrentCommand(long chatId, BotCommandType currentCommand);
    Conversation GetCurrentConversation(long chatId);
    BotCommandType GetCurrentCommand(long chatId);
}