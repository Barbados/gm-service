using Gm.Application.UseCases.Subscriptions.Queries;
using Gm.Domain.Aggregates.SubscriptionAggregate;
using Gm.Infrastructure.TelegramBot.Abstract;
using MediatR;
using Quartz;

namespace Gm.Infrastructure.Scheduler.Jobs;

public class GmSenderJob(ISenderService senderService, IMediator mediator) : IJob
{
    private static readonly SubscriptionTopic SubscriptionTopic = SubscriptionTopic.GoodMorning;
    public static readonly JobKey Key = new(nameof(GmSenderJob));
    
    public async Task Execute(IJobExecutionContext context)
    {
        var subscriptions = await mediator.Send(new GetSubscriptionsByTopicQuery(SubscriptionTopic));

        foreach (var sub in subscriptions)
        {
            await senderService.SendAsync(sub.ChatId, SubscriptionTopic, default);
        }
    }
}