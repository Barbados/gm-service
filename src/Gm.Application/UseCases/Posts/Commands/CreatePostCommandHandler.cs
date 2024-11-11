using Ardalis.SharedKernel;
using Gm.Domain.Aggregates.PostAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Gm.Application.UseCases.Posts.Commands;

public class CreatePostCommandHandler(IRepository<Post> repository, ILogger<CreatePostCommandHandler> logger) 
    : IRequestHandler<CreatePostCommand, Post>
{
    public async Task<Post> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var newPost = new Post
        {
            SendDate = request.PostDate,
            Text = request.Text
        };
        
        logger.LogInformation("Creating a new post has started");
        var result = await repository.AddAsync(newPost, cancellationToken);
        logger.LogInformation($"Creating a new post has finished. Post id: {result.Id}");
        
        return result;
    }
}