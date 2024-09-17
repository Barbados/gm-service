using Gm.Infrastructure.TelegramBot.Abstract;
using Quartz;

namespace Gm.Infrastructure.Scheduler.Jobs;

public class GmSenderJob(ISenderService senderService) : IJob
{
    public ISenderService SenderService { get; } = senderService;

    public async Task Execute(IJobExecutionContext context)
    {
        await senderService.SendAsync(default);
    }
}