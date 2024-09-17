using Gm.Infrastructure.TelegramBot.Abstract;
using Quartz;

namespace Gm.Infrastructure.Scheduler.Jobs;

public class GmSenderJob(ISenderService senderService) : IJob
{
    public static readonly JobKey Key = new(nameof(GmSenderJob));
    public async Task Execute(IJobExecutionContext context)
    {
        await senderService.SendAsync(default);
    }
}