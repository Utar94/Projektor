using AutoMapper;
using Logitar.Identity.Core;
using MediatR;
using Projektor.Core.Issues.Models;
using Projektor.Core.Repositories;

namespace Projektor.Core.Issues.Commands
{
  internal class UpdateIssueCommandHandler : SaveIssueHandler, IRequestHandler<UpdateIssueCommand, IssueModel>
  {
    private readonly IIssueRepository _issueRepository;
    private readonly IUserContext _userContext;

    public UpdateIssueCommandHandler(
      IIssueRepository issueRepository,
      IMapper mapper,
      IUserContext userContext
    ) : base(issueRepository, mapper)
    {
      _issueRepository = issueRepository;
      _userContext = userContext;
    }

    public async Task<IssueModel> Handle(UpdateIssueCommand request, CancellationToken cancellationToken)
    {
      Issue issue = await _issueRepository
        .GetAsync(request.Id, readOnly: false, cancellationToken)
        ?? throw new EntityNotFoundException<Issue>(request.Id);

      if (issue.CreatedById != _userContext.Id)
      {
        throw new UnauthorizedOperationException<Issue>(issue, _userContext.Id);
      }

      issue.Update(_userContext.Id);

      return await SaveAsync(issue, request.Payload, cancellationToken);
    }
  }
}
