using AutoMapper;
using Logitar.Identity.Core;
using MediatR;
using Projektor.Core.Comments.Models;
using Projektor.Core.Models;
using Projektor.Core.Repositories;

namespace Projektor.Core.Comments.Queries
{
  internal class GetCommentsQueryHandler : IRequestHandler<GetCommentsQuery, ListModel<CommentModel>>
  {
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;
    private readonly IUserContext _userContext;

    public GetCommentsQueryHandler(
      ICommentRepository commentRepository,
      IMapper mapper,
      IUserContext userContext
    )
    {
      _commentRepository = commentRepository;
      _mapper = mapper;
      _userContext = userContext;
    }

    public async Task<ListModel<CommentModel>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
    {
      PagedList<Comment> comments = await _commentRepository.GetPagedAsync(
        _userContext.Id,
        request.Deleted,
        request.IssueId,
        request.Sort,
        request.Desc,
        request.Index,
        request.Count,
        readOnly: true,
        cancellationToken
      );

      return new ListModel<CommentModel>(
        _mapper.Map<IEnumerable<CommentModel>>(comments),
        comments.Total
      );
    }
  }
}
