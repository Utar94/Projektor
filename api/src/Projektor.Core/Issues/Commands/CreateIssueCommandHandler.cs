using AutoMapper;
using Logitar.Identity.Core;
using MediatR;
using Projektor.Core.Issues.Models;
using Projektor.Core.Repositories;

namespace Projektor.Core.Issues.Commands
{
  internal class CreateIssueCommandHandler : SaveIssueHandler, IRequestHandler<CreateIssueCommand, IssueModel>
  {
    private readonly IIssueRepository _issueRepository;
    private readonly IIssueTypeRepository _issueTypeRepository;
    private readonly IUserContext _userContext;

    public CreateIssueCommandHandler(
      IIssueRepository issueRepository,
      IIssueTypeRepository issueTypeRepository,
      IMapper mapper,
      IUserContext userContext
    ) : base(issueRepository, mapper)
    {
      _issueRepository = issueRepository;
      _issueTypeRepository = issueTypeRepository;
      _userContext = userContext;
    }

    public async Task<IssueModel> Handle(CreateIssueCommand request, CancellationToken cancellationToken)
    {
      IssueType type = await _issueTypeRepository
        .GetAsync(request.Payload.TypeId, readOnly: false, cancellationToken)
        ?? throw new EntityNotFoundException<IssueType>(request.Payload.TypeId, nameof(request.Payload.TypeId));

      if (type.CreatedById != _userContext.Id)
      {
        throw new UnauthorizedOperationException<IssueType>(type, _userContext.Id);
      }

      int number = (await _issueRepository
        .GetMaximumNumberAsync(type.ProjectId, cancellationToken) ?? 0) + 1;

      var issue = new Issue(number, type, _userContext.Id);

      return await SaveAsync(issue, request.Payload, cancellationToken);
    }
  }
}
