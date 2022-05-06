using AutoMapper;
using Logitar.Identity.Core;
using MediatR;
using Projektor.Core.Issues.Models;
using Projektor.Core.Repositories;

namespace Projektor.Core.Issues.Queries
{
  internal class GetIssueQueryHandler : IRequestHandler<GetIssueQuery, IssueModel>
  {
    private readonly IIssueRepository _issueRepository;
    private readonly IMapper _mapper;
    private readonly IUserContext _userContext;

    public GetIssueQueryHandler(
      IIssueRepository issueRepository,
      IMapper mapper,
      IUserContext userContext
    )
    {
      _issueRepository = issueRepository;
      _mapper = mapper;
      _userContext = userContext;
    }

    public async Task<IssueModel> Handle(GetIssueQuery request, CancellationToken cancellationToken)
    {
      Issue issue;
      if (Guid.TryParse(request.Id, out Guid uuid))
      {
        issue = await _issueRepository
          .GetAsync(uuid, readOnly: true, cancellationToken)
          ?? throw new EntityNotFoundException<Issue>(request.Id);
      }
      else
      {
        string[] values = request.Id.Split('-');
        if (values.Length != 2 || !int.TryParse(values[1], out int number))
        {
          throw new InvalidIssueKeyException(request.Id);
        }

        issue = await _issueRepository
          .GetAsync(values[0], number, readOnly: true, cancellationToken)
          ?? throw new EntityNotFoundException<Issue>(request.Id);
      }

      if (issue.CreatedById != _userContext.Id)
      {
        throw new UnauthorizedOperationException<Issue>(issue, _userContext.Id);
      }

      return _mapper.Map<IssueModel>(issue);
    }
  }
}
