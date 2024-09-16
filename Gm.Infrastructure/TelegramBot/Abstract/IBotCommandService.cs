using Telegram.Bot.Types;

namespace Gm.Infrastructure.TelegramBot.Abstract;

public interface IBotCommandService
{
    Task HandleAsync(Message message);
}