using Projektor.Core.Issues;

namespace Projektor.Core.Repositories
{
  public interface IIssueTypeRepository
  {
    Task<IssueType?> GetAsync(Guid uuid, bool readOnly = false, CancellationToken cancellationToken = default);
    Task<PagedList<IssueType>> GetPagedAsync(
      Guid userId,
      bool? deleted = null,
      Guid? projectUuid = null,
      string? search = null,
      IssueTypeSort? sort = null,
      bool desc = false,
      int? index = null,
      int? count = null,
      bool readOnly = false,
      CancellationToken cancellationToken = default
    );
    Task SaveAsync(IssueType issueType, CancellationToken cancellationToken = default);
  }
}
