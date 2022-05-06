using Projektor.Core.Projects;

namespace Projektor.Core.Repositories
{
  public interface IProjectRepository
  {
    Task<Project?> GetAsync(string key, bool readOnly = false, CancellationToken cancellationToken = default);
    Task<Project?> GetAsync(Guid uuid, bool readOnly = false, CancellationToken cancellationToken = default);
    Task<PagedList<Project>> GetPagedAsync(
      Guid userId,
      bool? deleted = null,
      string? search = null,
      ProjectSort? sort = null,
      bool desc = false,
      int? index = null,
      int? count = null,
      bool readOnly = false,
      CancellationToken cancellationToken = default
    );
    Task SaveAsync(Project project, CancellationToken cancellationToken = default);
  }
}
