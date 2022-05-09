using AutoMapper;
using Logitar.Identity.Core;
using MediatR;
using Projektor.Core.Comments.Models;
using Projektor.Core.Issues;
using Projektor.Core.Repositories;

namespace Projektor.Core.Comments.Commands
{
  internal class CreateCommentCommandHandler : SaveCommentHandler, IRequestHandler<CreateCommentCommand, CommentModel>
  {
    private readonly IIssueRepository _issueRepository;
    private readonly IUserContext _userContext;

    public CreateCommentCommandHandler(
      ICommentRepository commentRepository,
      IIssueRepository issueRepository,
      IMapper mapper,
      IUserContext userContext
    ) : base(commentRepository, mapper)
    {
      _issueRepository = issueRepository;
      _userContext = userContext;
    }

    public async Task<CommentModel> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
      Issue issue = await _issueRepository
        .GetAsync(request.Payload.IssueId, readOnly: false, cancellationToken)
        ?? throw new EntityNotFoundException<Issue>(request.Payload.IssueId, nameof(request.Payload.IssueId));

      if (issue.CreatedById != _userContext.Id)
      {
        throw new UnauthorizedOperationException<Issue>(issue, _userContext.Id);
      }

      var comment = new Comment(issue, _userContext.Id);

      return await SaveAsync(comment, request.Payload, cancellationToken);
    }
  }
}
