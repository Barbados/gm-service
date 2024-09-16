using Ardalis.SharedKernel;
using Ardalis.Specification.EntityFrameworkCore;
using Gm.Infrastructure.Data.Contexts;

namespace Gm.Infrastructure.Data;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}