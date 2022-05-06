using AutoMapper;
using Logitar.Identity.Core;
using MediatR;
using Projektor.Core.Issues.Models;
using Projektor.Core.Repositories;

namespace Projektor.Core.Issues.Queries
{
  internal class GetIssueTypeQueryHandler : IRequestHandler<GetIssueTypeQuery, IssueTypeModel>
  {
    private readonly IIssueTypeRepository _issueTypeRepository;
    private readonly IMapper _mapper;
    private readonly IUserContext _userContext;

    public GetIssueTypeQueryHandler(
      IIssueTypeRepository issueTypeRepository,
      IMapper mapper,
      IUserContext userContext
    )
    {
      _issueTypeRepository = issueTypeRepository;
      _mapper = mapper;
      _userContext = userContext;
    }

    public async Task<IssueTypeModel> Handle(GetIssueTypeQuery request, CancellationToken cancellationToken)
    {
      IssueType issueType = await _issueTypeRepository
        .GetAsync(request.Id, readOnly: true, cancellationToken)
        ?? throw new EntityNotFoundException<IssueType>(request.Id);

      if (issueType.CreatedById != _userContext.Id)
      {
        throw new UnauthorizedOperationException<IssueType>(issueType, _userContext.Id);
      }

      return _mapper.Map<IssueTypeModel>(issueType);
    }
  }
}
