using MediatR;
using Projektor.Core.Comments.Models;
using Projektor.Core.Comments.Payloads;

namespace Projektor.Core.Comments.Commands
{
  public class CreateCommentCommand : IRequest<CommentModel>
  {
    public CreateCommentCommand(CreateCommentPayload payload)
    {
      Payload = payload ?? throw new ArgumentNullException(nameof(payload));
    }

    public CreateCommentPayload Payload { get; }
  }
}
