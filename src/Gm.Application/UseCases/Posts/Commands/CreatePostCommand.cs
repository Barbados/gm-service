using Gm.Domain.Aggregates.PostAggregate;
using MediatR;

namespace Gm.Application.UseCases.Posts.Commands;

public record CreatePostCommand(DateOnly PostDate, string? Text) : IRequest<Post>;