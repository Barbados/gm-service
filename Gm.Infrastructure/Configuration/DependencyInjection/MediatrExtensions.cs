using Gm.Application.UseCases.Subscribers.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gm.Infrastructure.Configuration.DependencyInjection;

public static class MediatrExtensions
{
    public static IServiceCollection ConfigureMediatr(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<CreateSubscriberCommandHandler>();
        });

        return services;
    }
}