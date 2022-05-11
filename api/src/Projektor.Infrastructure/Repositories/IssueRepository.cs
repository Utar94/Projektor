using Microsoft.EntityFrameworkCore;
using Projektor.Core;
using Projektor.Core.Issues;
using Projektor.Core.Repositories;
using Projektor.Infrastructure.Extensions;

namespace Projektor.Infrastructure.Repositories
{
  internal class IssueRepository : RepositoryBase<Issue>, IIssueRepository
  {
    public IssueRepository(ProjektorDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Issue?> GetAsync(Guid uuid, bool readOnly = false, CancellationToken cancellationToken = default)
    {
      return await DbContext.Issues
        .ApplyTracking(readOnly)
        .Include(x => x.Project)
        .Include(x => x.Type)
        .SingleOrDefaultAsync(x => x.Uuid == uuid, cancellationToken);
    }

    public async Task<Issue?> GetAsync(string projectKey, int number, bool readOnly = false, CancellationToken cancellationToken = default)
    {
      ArgumentNullException.ThrowIfNull(projectKey);

      return await DbContext.Issues
        .ApplyTracking(readOnly)
        .Include(x => x.Project)
        .Include(x => x.Type)
        .SingleOrDefaultAsync(x => x.Project != null && x.Project.Key == projectKey.ToLowerInvariant()
          && x.Number == number, cancellationToken);
    }

    public async Task<int?> GetMaximumNumberAsync(int projectId, CancellationToken cancellationToken = default)
    {
      return await DbContext.Issues
        .AsNoTracking()
        .Where(x => x.ProjectId == projectId)
        .MaxAsync(x => (int?)x.Number, cancellationToken);
    }

    public async Task<PagedList<Issue>> GetPagedAsync(
      Guid userId,
      bool? deleted = null,
      Priority? priority = null,
      Guid? projectId = null,
      string? search = null,
      Guid? typeId = null,
      IssueSort? sort = null,
      bool desc = false,
      int? index = null,
      int? count = null,
      bool readOnly = false,
      CancellationToken cancellationToken = default
    )
    {
      IQueryable<Issue> query = DbContext.Issues
        .ApplyTracking(readOnly)
        .Include(x => x.Project)
        .Include(x => x.Type)
        .Where(x => x.CreatedById == userId);

      if (deleted.HasValue)
      {
        query = query.Where(x => x.Deleted == deleted);
      }
      if (priority.HasValue)
      {
        query = query.Where(x => x.Priority == priority.Value);
      }
      if (projectId.HasValue)
      {
        query = query.Where(x => x.Project != null && x.Project.Uuid == projectId.Value);
      }
      if (search != null)
      {
        query = query.Where(x => x.Name.Contains(search));
      }
      if (typeId.HasValue)
      {
        query = query.Where(x => x.Type != null && x.Type.Uuid == typeId.Value);
      }

      long total = await query.LongCountAsync(cancellationToken);

      if (sort.HasValue)
      {
        query = sort.Value switch
        {
          IssueSort.Key => desc
            ? query.OrderByDescending(x => x.Project!.Key).ThenByDescending(x => x.Number)
            : query.OrderBy(x => x.Project!.Key).ThenBy(x => x.Number),
          IssueSort.Name => desc ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name),
          IssueSort.Priority => desc ? query.OrderByDescending(x => x.Priority) : query.OrderBy(x => x.Priority),
          IssueSort.Type => desc ? query.OrderByDescending(x => x.Type!.Name) : query.OrderBy(x => x.Type!.Name),
          IssueSort.UpdatedAt => desc ? query.OrderByDescending(x => x.UpdatedAt ?? x.CreatedAt) : query.OrderBy(x => x.UpdatedAt ?? x.CreatedAt),
          _ => throw new ArgumentException($"The sort \"{sort}\" is not valid.", nameof(sort)),
        };
      }

      query = query.ApplyPaging(index, count);

      Issue[] issues = await query.ToArrayAsync(cancellationToken);

      return new PagedList<Issue>(issues, total);
    }
  }
}
