using AutoMapper;
using Projektor.Core.Comments.Models;
using Projektor.Core.Comments.Payloads;
using Projektor.Core.Repositories;

namespace Projektor.Core.Comments.Commands
{
  internal abstract class SaveCommentHandler
  {
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;

    protected SaveCommentHandler(ICommentRepository commentRepository, IMapper mapper)
    {
      _commentRepository = commentRepository;
      _mapper = mapper;
    }

    protected async Task<CommentModel> SaveAsync(Comment comment, SaveCommentPayload payload, CancellationToken cancellationToken = default)
    {
      comment.Text = payload.Text.Trim();

      await _commentRepository.SaveAsync(comment, cancellationToken);

      return _mapper.Map<CommentModel>(comment);
    }
  }
}
