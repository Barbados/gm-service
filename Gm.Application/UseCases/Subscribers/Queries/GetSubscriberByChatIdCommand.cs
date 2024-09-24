using Gm.Domain.Aggregates.SubscriberAggregate;
using MediatR;

namespace Gm.Application.UseCases.Subscribers.Queries;

public sealed record GetSubscriberByChatIdCommand(long ChatId) : IRequest<Subscriber?>;