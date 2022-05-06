using Microsoft.EntityFrameworkCore;
using Projektor.Core;
using Projektor.Core.Repositories;
using Projektor.Core.Worklogs;
using Projektor.Infrastructure.Extensions;

namespace Projektor.Infrastructure.Repositories
{
  internal class WorklogRepository : RepositoryBase<Worklog>, IWorklogRepository
  {
    public WorklogRepository(ProjektorDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Worklog?> GetAsync(Guid uuid, bool readOnly = false, CancellationToken cancellationToken = default)
    {
      return await DbContext.Worklogs
        .ApplyTracking(readOnly)
        .SingleOrDefaultAsync(x => x.Uuid == uuid, cancellationToken);
    }

    public async Task<PagedList<Worklog>> GetPagedAsync(
      Guid userId,
      bool? deleted = null,
      Guid? issueId = null,
      WorklogSort? sort = null,
      bool desc = false,
      int? index = null,
      int? count = null,
      bool readOnly = false,
      CancellationToken cancellationToken = default
    )
    {
      IQueryable<Worklog> query = DbContext.Worklogs
        .ApplyTracking(readOnly)
        .Include(x => x.Issue)
        .Where(x => x.CreatedById == userId);

      if (deleted.HasValue)
      {
        query = query.Where(x => x.Deleted == deleted);
      }
      if (issueId.HasValue)
      {
        query = query.Where(x => x.Issue != null && x.Issue.Uuid == issueId.Value);
      }

      long total = await query.LongCountAsync(cancellationToken);

      if (sort.HasValue)
      {
        query = sort.Value switch
        {
          WorklogSort.EndedAt => desc ? query.OrderByDescending(x => x.EndedAt) : query.OrderBy(x => x.EndedAt),
          WorklogSort.StartedAt => desc ? query.OrderByDescending(x => x.StartedAt) : query.OrderBy(x => x.StartedAt),
          WorklogSort.UpdatedAt => desc ? query.OrderByDescending(x => x.UpdatedAt ?? x.CreatedAt) : query.OrderBy(x => x.UpdatedAt ?? x.CreatedAt),
          _ => throw new ArgumentException($"The sort \"{sort}\" is not valid.", nameof(sort)),
        };
      }

      query = query.ApplyPaging(index, count);

      Worklog[] worklogs = await query.ToArrayAsync(cancellationToken);

      return new PagedList<Worklog>(worklogs, total);
    }
  }
}
