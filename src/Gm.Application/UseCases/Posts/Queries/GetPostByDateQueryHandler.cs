using Ardalis.SharedKernel;
using Gm.Domain.Aggregates.PostAggregate;
using Gm.Domain.Aggregates.PostAggregate.Specifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Gm.Application.UseCases.Posts.Queries;

public class GetPostByDateQueryHandler(IRepository<Post> repository,
    ILogger<GetPostByDateQueryHandler> logger) : IRequestHandler<GetPostByDateQuery, Post?>
{
    public async Task<Post?> Handle(GetPostByDateQuery request, CancellationToken cancellationToken)
    {
        var post = await repository.SingleOrDefaultAsync(new GetPostByDateSpec(request.Date), cancellationToken);

        return post;
    }
}