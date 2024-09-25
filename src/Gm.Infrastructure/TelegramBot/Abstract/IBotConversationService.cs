using Telegram.Bot.Types;

namespace Gm.Infrastructure.TelegramBot.Abstract;

public interface IBotConversationService
{
    Task HandleAsync(Message message);
}