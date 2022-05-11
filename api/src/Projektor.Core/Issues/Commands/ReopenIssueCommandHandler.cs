using AutoMapper;
using Logitar.Identity.Core;
using MediatR;
using Projektor.Core.Issues.Models;
using Projektor.Core.Repositories;

namespace Projektor.Core.Issues.Commands
{
  internal class ReopenIssueCommandHandler : IRequestHandler<ReopenIssueCommand, IssueModel>
  {
    private readonly IIssueRepository _issueRepository;
    private readonly IMapper _mapper;
    private readonly IUserContext _userContext;

    public ReopenIssueCommandHandler(
      IIssueRepository issueRepository,
      IMapper mapper,
      IUserContext userContext
    )
    {
      _issueRepository = issueRepository;
      _mapper = mapper;
      _userContext = userContext;
    }

    public async Task<IssueModel> Handle(ReopenIssueCommand request, CancellationToken cancellationToken)
    {
      Issue issue = await _issueRepository
        .GetAsync(request.Id, readOnly: false, cancellationToken)
        ?? throw new EntityNotFoundException<Issue>(request.Id);

      if (issue.CreatedById != _userContext.Id)
      {
        throw new UnauthorizedOperationException<Issue>(issue, _userContext.Id);
      }
      else if (!issue.IsClosed)
      {
        throw new IssueNotClosedException(issue);
      }

      issue.Reopen();

      await _issueRepository.SaveAsync(issue, cancellationToken);

      return _mapper.Map<IssueModel>(issue);
    }
  }
}
