using Projektor.Core.Comments;

namespace Projektor.Core.Repositories
{
  public interface ICommentRepository
  {
    Task<Comment?> GetAsync(Guid uuid, bool readOnly = false, CancellationToken cancellationToken = default);
    Task<PagedList<Comment>> GetPagedAsync(
      Guid userId,
      bool? deleted = null,
      Guid? issueId = null,
      CommentSort? sort = null,
      bool desc = false,
      int? index = null,
      int? count = null,
      bool readOnly = false,
      CancellationToken cancellationToken = default
    );
    Task SaveAsync(Comment comment, CancellationToken cancellationToken = default);
  }
}
