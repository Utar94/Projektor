using Projektor.Core.Worklogs;

namespace Projektor.Core.Repositories
{
  public interface IWorklogRepository
  {
    Task<Worklog?> GetAsync(Guid uuid, bool readOnly = false, CancellationToken cancellationToken = default);
    Task<PagedList<Worklog>> GetPagedAsync(
      Guid userId,
      bool? deleted = null,
      Guid? issueId = null,
      WorklogSort? sort = null,
      bool desc = false,
      int? index = null,
      int? count = null,
      bool readOnly = false,
      CancellationToken cancellationToken = default
    );
    Task SaveAsync(Worklog worklog, CancellationToken cancellationToken = default);
  }
}
