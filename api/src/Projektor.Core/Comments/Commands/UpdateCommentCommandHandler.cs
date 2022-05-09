using AutoMapper;
using Logitar.Identity.Core;
using MediatR;
using Projektor.Core.Comments.Models;
using Projektor.Core.Repositories;

namespace Projektor.Core.Comments.Commands
{
  internal class UpdateCommentCommandHandler : SaveCommentHandler, IRequestHandler<UpdateCommentCommand, CommentModel>
  {
    private readonly IUserContext _userContext;
    private readonly ICommentRepository _commentRepository;

    public UpdateCommentCommandHandler(
      ICommentRepository commentRepository,
      IMapper mapper,
      IUserContext userContext
    ) : base(commentRepository, mapper)
    {
      _commentRepository = commentRepository;
      _userContext = userContext;
    }

    public async Task<CommentModel> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
      Comment comment = await _commentRepository
        .GetAsync(request.Id, readOnly: false, cancellationToken)
        ?? throw new EntityNotFoundException<Comment>(request.Id);

      if (comment.CreatedById != _userContext.Id)
      {
        throw new UnauthorizedOperationException<Comment>(comment, _userContext.Id);
      }

      comment.Update(_userContext.Id);

      return await SaveAsync(comment, request.Payload, cancellationToken);
    }
  }
}
