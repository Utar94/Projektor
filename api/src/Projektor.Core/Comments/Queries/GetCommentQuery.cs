using MediatR;
using Projektor.Core.Comments.Models;

namespace Projektor.Core.Comments.Queries
{
  public class GetCommentQuery : IRequest<CommentModel>
  {
    public GetCommentQuery(Guid id)
    {
      Id = id;
    }

    public Guid Id { get; }
  }
}
