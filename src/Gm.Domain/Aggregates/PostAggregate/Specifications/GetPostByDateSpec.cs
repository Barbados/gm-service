using Ardalis.Specification;

namespace Gm.Domain.Aggregates.PostAggregate.Specifications;

public sealed class GetPostByDateSpec : SingleResultSpecification<Post>
{
    public GetPostByDateSpec(DateOnly date)
    {
        Query.Where(p => p.SendDate == date);
    }
}