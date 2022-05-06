using Projektor.Core;

namespace Projektor.Infrastructure.Repositories
{
  internal abstract class RepositoryBase<T> where T : Aggregate
  {
    protected RepositoryBase(ProjektorDbContext dbContext)
    {
      DbContext = dbContext;
    }

    protected ProjektorDbContext DbContext { get; }

    public async Task SaveAsync(T entity, CancellationToken cancellationToken = default)
    {
      ArgumentNullException.ThrowIfNull(entity);

      if (entity.Id == 0)
      {
        DbContext.Add(entity);
      }
      else
      {
        DbContext.Update(entity);
      }

      await DbContext.SaveChangesAsync(cancellationToken);
    }
  }
}
