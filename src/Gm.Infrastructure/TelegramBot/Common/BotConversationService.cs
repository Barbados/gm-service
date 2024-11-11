using Gm.Infrastructure.TelegramBot.Abstract;
using Gm.Infrastructure.TelegramBot.Model;
using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Gm.Infrastructure.TelegramBot.Common;

public class BotConversationService(
    ITelegramBotClient botClient,
    IMediator mediator, 
    IConversationHelper conversationHelper) : IBotConversationService
{
    public async Task HandleAsync(Message message)
    {
        var chatId = message.Chat.Id;
        var currentCommand = conversationHelper.GetCurrentCommand(chatId);
        if (currentCommand.Name is not nameof(BotCommandType.None))
        {
            await (currentCommand.Name switch
            {
                nameof(BotCommandType.AddPost) => HandleAddPostConversation(message),
                _ => Task.CompletedTask
            });
        }
    }
    
    private async Task HandleAddPostConversation(Message message)
    {
        var currentConversation = conversationHelper.GetCurrentConversation(message.Chat.Id);
        if (currentConversation.ConversationCounter >= 3)
        {
            currentConversation.ConversationCounter = 0;
            currentConversation.CurrentCommand = BotCommandType.None;
            return;
        }

        // Create a new user as a patient
        await botClient.SendTextMessageAsync(message.Chat, $"You posted: '{message.Text}'. Go on!");
    }
}