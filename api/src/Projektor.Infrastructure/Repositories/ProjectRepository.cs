using Microsoft.EntityFrameworkCore;
using Projektor.Core;
using Projektor.Core.Projects;
using Projektor.Core.Repositories;
using Projektor.Infrastructure.Extensions;

namespace Projektor.Infrastructure.Repositories
{
  internal class ProjectRepository : RepositoryBase<Project>, IProjectRepository
  {
    public ProjectRepository(ProjektorDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Project?> GetAsync(string key, bool readOnly = false, CancellationToken cancellationToken = default)
    {
      ArgumentNullException.ThrowIfNull(key);

      return await DbContext.Projects
        .ApplyTracking(readOnly)
        .SingleOrDefaultAsync(x => x.Key == key.ToLowerInvariant(), cancellationToken);
    }

    public async Task<Project?> GetAsync(Guid uuid, bool readOnly = false, CancellationToken cancellationToken = default)
    {
      return await DbContext.Projects
        .ApplyTracking(readOnly)
        .SingleOrDefaultAsync(x => x.Uuid == uuid, cancellationToken);
    }

    public async Task<PagedList<Project>> GetPagedAsync(
      Guid userId,
      bool? deleted = null,
      string? search = null,
      ProjectSort? sort = null,
      bool desc = false,
      int? index = null,
      int? count = null,
      bool readOnly = false,
      CancellationToken cancellationToken = default
    )
    {
      IQueryable<Project> query = DbContext.Projects
        .ApplyTracking(readOnly)
        .Where(x => x.CreatedById == userId);

      if (deleted.HasValue)
      {
        query = query.Where(x => x.Deleted == deleted);
      }
      if (search != null)
      {
        query = query.Where(x => x.Key.Contains(search) || x.Name.Contains(search));
      }

      long total = await query.LongCountAsync(cancellationToken);

      if (sort.HasValue)
      {
        query = sort.Value switch
        {
          ProjectSort.Key => desc ? query.OrderByDescending(x => x.Key) : query.OrderBy(x => x.Key),
          ProjectSort.Name => desc ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name),
          ProjectSort.UpdatedAt => desc ? query.OrderByDescending(x => x.UpdatedAt ?? x.CreatedAt) : query.OrderBy(x => x.UpdatedAt ?? x.CreatedAt),
          _ => throw new ArgumentException($"The sort \"{sort}\" is not valid.", nameof(sort)),
        };
      }

      query = query.ApplyPaging(index, count);

      Project[] projects = await query.ToArrayAsync(cancellationToken);

      return new PagedList<Project>(projects, total);
    }
  }
}
