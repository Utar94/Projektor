using AutoMapper;
using Logitar.Identity.Core;
using MediatR;
using Projektor.Core.Issues.Models;
using Projektor.Core.Repositories;

namespace Projektor.Core.Issues.Commands
{
  internal class UpdateIssueTypeCommandHandler : SaveIssueTypeHandler, IRequestHandler<UpdateIssueTypeCommand, IssueTypeModel>
  {
    private readonly IIssueTypeRepository _issueTypeRepository;
    private readonly IUserContext _userContext;

    public UpdateIssueTypeCommandHandler(
      IIssueTypeRepository issueTypeRepository,
      IMapper mapper,
      IUserContext userContext
    ) : base(issueTypeRepository, mapper)
    {
      _issueTypeRepository = issueTypeRepository;
      _userContext = userContext;
    }

    public async Task<IssueTypeModel> Handle(UpdateIssueTypeCommand request, CancellationToken cancellationToken)
    {
      IssueType issueType = await _issueTypeRepository
        .GetAsync(request.Id, readOnly: false, cancellationToken)
        ?? throw new EntityNotFoundException<IssueType>(request.Id);

      if (issueType.CreatedById != _userContext.Id)
      {
        throw new UnauthorizedOperationException<IssueType>(issueType, _userContext.Id);
      }

      issueType.Update(_userContext.Id);

      return await SaveAsync(issueType, request.Payload, cancellationToken);
    }
  }
}
