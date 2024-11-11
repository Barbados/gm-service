using Ardalis.SharedKernel;
using EntityBase = Gm.Domain.Aggregates.SeedWork.EntityBase;

namespace Gm.Domain.Aggregates.PostAggregate;

public class Post : EntityBase, IAggregateRoot
{
    public string? Text { get; set; }
    
    public DateOnly SendDate { get; set; }
}