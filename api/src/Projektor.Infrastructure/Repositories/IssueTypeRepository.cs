using Microsoft.EntityFrameworkCore;
using Projektor.Core;
using Projektor.Core.Issues;
using Projektor.Core.Repositories;
using Projektor.Infrastructure.Extensions;

namespace Projektor.Infrastructure.Repositories
{
  internal class IssueTypeRepository : RepositoryBase<IssueType>, IIssueTypeRepository
  {
    public IssueTypeRepository(ProjektorDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IssueType?> GetAsync(Guid uuid, bool readOnly = false, CancellationToken cancellationToken = default)
    {
      return await DbContext.IssueTypes
        .ApplyTracking(readOnly)
        .Include(x => x.Project)
        .SingleOrDefaultAsync(x => x.Uuid == uuid, cancellationToken);
    }

    public async Task<PagedList<IssueType>> GetPagedAsync(
      Guid userId,
      bool? deleted = null,
      Guid? projectId = null,
      string? search = null,
      IssueTypeSort? sort = null,
      bool desc = false,
      int? index = null,
      int? count = null,
      bool readOnly = false,
      CancellationToken cancellationToken = default
    )
    {
      IQueryable<IssueType> query = DbContext.IssueTypes
        .ApplyTracking(readOnly)
        .Include(x => x.Project)
        .Where(x => x.CreatedById == userId);

      if (deleted.HasValue)
      {
        query = query.Where(x => x.Deleted == deleted);
      }
      if (projectId.HasValue)
      {
        query = query.Where(x => x.Project != null && x.Project.Uuid == projectId.Value);
      }
      if (search != null)
      {
        query = query.Where(x => x.Name.Contains(search));
      }

      long total = await query.LongCountAsync(cancellationToken);

      if (sort.HasValue)
      {
        query = sort.Value switch
        {
          IssueTypeSort.Name => desc ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name),
          IssueTypeSort.Project => desc ? query.OrderByDescending(x => x.Project!.Name) : query.OrderBy(x => x.Project!.Name),
          IssueTypeSort.UpdatedAt => desc ? query.OrderByDescending(x => x.UpdatedAt ?? x.CreatedAt) : query.OrderBy(x => x.UpdatedAt ?? x.CreatedAt),
          _ => throw new ArgumentException($"The sort \"{sort}\" is not valid.", nameof(sort)),
        };
      }

      query = query.ApplyPaging(index, count);

      IssueType[] issueTypes = await query.ToArrayAsync(cancellationToken);

      return new PagedList<IssueType>(issueTypes, total);
    }
  }
}
