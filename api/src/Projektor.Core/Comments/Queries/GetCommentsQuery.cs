using MediatR;
using Projektor.Core.Comments.Models;
using Projektor.Core.Models;

namespace Projektor.Core.Comments.Queries
{
  public class GetCommentsQuery : IRequest<ListModel<CommentModel>>
  {
    public bool? Deleted { get; set; }
    public Guid? IssueId { get; set; }

    public CommentSort? Sort { get; set; }
    public bool Desc { get; set; }

    public int? Index { get; set; }
    public int? Count { get; set; }
  }
}
