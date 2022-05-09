using AutoMapper;
using Logitar.Identity.Core;
using MediatR;
using Projektor.Core.Comments.Models;
using Projektor.Core.Repositories;

namespace Projektor.Core.Comments.Queries
{
  internal class GetCommentQueryHandler : IRequestHandler<GetCommentQuery, CommentModel>
  {
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;
    private readonly IUserContext _userContext;

    public GetCommentQueryHandler(
      ICommentRepository commentRepository,
      IMapper mapper,
      IUserContext userContext
    )
    {
      _commentRepository = commentRepository;
      _mapper = mapper;
      _userContext = userContext;
    }

    public async Task<CommentModel> Handle(GetCommentQuery request, CancellationToken cancellationToken)
    {
      Comment comment = await _commentRepository
        .GetAsync(request.Id, readOnly: true, cancellationToken)
        ?? throw new EntityNotFoundException<Comment>(request.Id);

      if (comment.CreatedById != _userContext.Id)
      {
        throw new UnauthorizedOperationException<Comment>(comment, _userContext.Id);
      }

      return _mapper.Map<CommentModel>(comment);
    }
  }
}
