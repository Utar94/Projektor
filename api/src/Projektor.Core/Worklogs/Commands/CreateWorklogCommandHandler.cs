using AutoMapper;
using Logitar.Identity.Core;
using MediatR;
using Projektor.Core.Issues;
using Projektor.Core.Repositories;
using Projektor.Core.Worklogs.Models;

namespace Projektor.Core.Worklogs.Commands
{
  internal class CreateWorklogCommandHandler : SaveWorklogHandler, IRequestHandler<CreateWorklogCommand, WorklogModel>
  {
    private readonly IIssueRepository _issueRepository;
    private readonly IUserContext _userContext;

    public CreateWorklogCommandHandler(
      IIssueRepository issueRepository,
      IMapper mapper,
      IUserContext userContext,
      IWorklogRepository worklogRepository
    ) : base(mapper, worklogRepository)
    {
      _issueRepository = issueRepository;
      _userContext = userContext;
    }

    public async Task<WorklogModel> Handle(CreateWorklogCommand request, CancellationToken cancellationToken)
    {
      Issue issue = await _issueRepository
        .GetAsync(request.Payload.IssueId, readOnly: false, cancellationToken)
        ?? throw new EntityNotFoundException<Issue>(request.Payload.IssueId, nameof(request.Payload.IssueId));

      if (issue.CreatedById != _userContext.Id)
      {
        throw new UnauthorizedOperationException<Issue>(issue, _userContext.Id);
      }

      var worklog = new Worklog(issue, _userContext.Id);

      return await SaveAsync(worklog, request.Payload, cancellationToken);
    }
  }
}
