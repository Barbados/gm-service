using System.Globalization;
using Gm.Application.UseCases.Posts.Queries;
using Gm.Domain.Aggregates.SubscriptionAggregate;
using Gm.Infrastructure.TelegramBot.Abstract;
using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Gm.Infrastructure.TelegramBot.Services.Core;

public class SenderService(ITelegramBotClient botClient,
    IMediator mediator) : ISenderService
{
    public async Task<Message> SendAsync(long chatId, SubscriptionTopic topic, CancellationToken token)
    {
        var sentMessage = await (topic.Name switch
        {
            nameof(SubscriptionTopic.GoodMorning) => botClient.SendTextMessageAsync(chatId, await ComposeGmMessage(), cancellationToken: token),
            _ => throw new ArgumentOutOfRangeException()
        });

        return sentMessage;
    }

    private async Task<string> ComposeGmMessage()
    {
        var culture = new CultureInfo("ru-RU");
        var message = string.Create(culture, $"Доброе утро!\nСегодня {DateTime.Today:D}\n");
        
        var post = await mediator.Send(new GetPostByDateQuery(DateOnly.FromDateTime(DateTime.Today)));
        message += post is not null ? $"\n{post.Text}\n" : string.Empty;
        
        message += $"\nХорошего дня!";

        return message;
    }
}