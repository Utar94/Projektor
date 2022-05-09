using MediatR;
using Projektor.Core.Comments.Models;
using Projektor.Core.Comments.Payloads;

namespace Projektor.Core.Comments.Commands
{
  public class UpdateCommentCommand : IRequest<CommentModel>
  {
    public UpdateCommentCommand(Guid id, UpdateCommentPayload payload)
    {
      Id = id;
      Payload = payload ?? throw new ArgumentNullException(nameof(payload));
    }

    public Guid Id { get; }
    public UpdateCommentPayload Payload { get; }
  }
}
