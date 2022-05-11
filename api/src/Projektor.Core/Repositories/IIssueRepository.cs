using Projektor.Core.Issues;

namespace Projektor.Core.Repositories
{
  public interface IIssueRepository
  {
    Task<Issue?> GetAsync(Guid uuid, bool readOnly = false, CancellationToken cancellationToken = default);
    Task<Issue?> GetAsync(string projectKey, int number, bool readOnly = false, CancellationToken cancellationToken = default);
    Task<int?> GetMaximumNumberAsync(int projectId, CancellationToken cancellationToken = default);
    Task<PagedList<Issue>> GetPagedAsync(
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
    );
    Task SaveAsync(Issue issue, CancellationToken cancellationToken = default);
  }
}
