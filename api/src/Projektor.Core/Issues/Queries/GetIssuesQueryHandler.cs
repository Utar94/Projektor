using AutoMapper;
using Logitar.Identity.Core;
using MediatR;
using Projektor.Core.Issues.Models;
using Projektor.Core.Models;
using Projektor.Core.Repositories;

namespace Projektor.Core.Issues.Queries
{
  internal class GetIssuesQueryHandler : IRequestHandler<GetIssuesQuery, ListModel<IssueModel>>
  {
    private readonly IIssueRepository _issueRepository;
    private readonly IMapper _mapper;
    private readonly IUserContext _userContext;

    public GetIssuesQueryHandler(
      IIssueRepository issueRepository,
      IMapper mapper,
      IUserContext userContext
    )
    {
      _issueRepository = issueRepository;
      _mapper = mapper;
      _userContext = userContext;
    }

    public async Task<ListModel<IssueModel>> Handle(GetIssuesQuery request, CancellationToken cancellationToken)
    {
      PagedList<Issue> issues = await _issueRepository.GetPagedAsync(
        _userContext.Id,
        request.Closed,
        request.Deleted,
        request.Priority,
        request.ProjectId,
        request.Resolution,
        request.Search,
        request.TypeId,
        request.Sort,
        request.Desc,
        request.Index,
        request.Count,
        readOnly: true,
        cancellationToken
      );

      return new ListModel<IssueModel>(
        _mapper.Map<IEnumerable<IssueModel>>(issues),
        issues.Total
      );
    }
  }
}
