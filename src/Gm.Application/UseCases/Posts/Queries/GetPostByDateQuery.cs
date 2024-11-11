using Gm.Domain.Aggregates.PostAggregate;
using MediatR;

namespace Gm.Application.UseCases.Posts.Queries;

public sealed record GetPostByDateQuery(DateOnly Date) : IRequest<Post?>;