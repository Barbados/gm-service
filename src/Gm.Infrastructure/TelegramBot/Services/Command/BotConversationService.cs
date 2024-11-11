using Gm.Application.UseCases.Posts.Commands;
using Gm.Infrastructure.TelegramBot.Abstract;
using Gm.Infrastructure.TelegramBot.Model;
using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Gm.Infrastructure.TelegramBot.Services.Command;

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
        switch (currentConversation.ConversationCounter)
        {
            case 0:
                currentConversation.ConversationCounter++;
                break;
            case 1:
                await mediator.Send(new CreatePostCommand(DateOnly.FromDateTime(DateTime.Today).AddDays(1), message.Text));
                await botClient.SendTextMessageAsync(message.Chat, $"You've just added a message to post tomorrow.");
                currentConversation.CurrentCommand = BotCommandType.None;
                currentConversation.ConversationCounter = 0;
                return;
        }
    }
}