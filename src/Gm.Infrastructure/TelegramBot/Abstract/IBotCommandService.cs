using Telegram.Bot.Types;

namespace Gm.Infrastructure.TelegramBot.Abstract;

/// <summary>
/// This interface describes working with commands handling
/// </summary>
public interface IBotCommandService
{
    Task HandleAsync(Message message);
}