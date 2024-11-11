using Gm.Infrastructure.TelegramBot.Abstract;
using Gm.Infrastructure.TelegramBot.Model;

namespace Gm.Infrastructure.TelegramBot.Common;

public class ConversationHelper : IConversationHelper
{
    private Dictionary<long, Conversation> ChatIdToConversation { get; } = new();

    public BotCommandType SetCurrentCommand(long chatId, BotCommandType currentCommand)
    {
        var conversationExists = ChatIdToConversation.TryGetValue(chatId, out var conversation);
        if (conversationExists && conversation is not null)
            conversation.CurrentCommand = currentCommand;
        else
            Initiate(chatId, currentCommand);

        return currentCommand;
    }

    public Conversation GetCurrentConversation(long chatId)
    {
        return ChatIdToConversation.TryGetValue(chatId, out var conversation)
            ? conversation
            : new Conversation { ChatId = chatId };
    }

    public BotCommandType GetCurrentCommand(long chatId)
    {
        return ChatIdToConversation.TryGetValue(chatId, out var conversation)
            ? conversation.CurrentCommand
            : BotCommandType.None;
    }
    
    private void Initiate(long chatId, BotCommandType? currentCommand = null)
    {
        var conversation = new Conversation
        {
            ChatId = chatId,
            CurrentCommand = currentCommand ?? BotCommandType.None
        };
        ChatIdToConversation.TryAdd(chatId, conversation);
    }
}

public class Conversation
{
    public long ChatId { get; set; }
    public BotCommandType CurrentCommand { get; set; } = BotCommandType.None;
    public bool IsFinished { get; set; }
    public int ConversationCounter { get; set; }
}