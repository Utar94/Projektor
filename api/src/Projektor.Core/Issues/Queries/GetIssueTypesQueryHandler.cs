using AutoMapper;
using Logitar.Identity.Core;
using MediatR;
using Projektor.Core.Issues.Models;
using Projektor.Core.Models;
using Projektor.Core.Repositories;

namespace Projektor.Core.Issues.Queries
{
  internal class GetIssueTypesQueryHandler : IRequestHandler<GetIssueTypesQuery, ListModel<IssueTypeModel>>
  {
    private readonly IIssueTypeRepository _issueTypeRepository;
    private readonly IMapper _mapper;
    private readonly IUserContext _userContext;

    public GetIssueTypesQueryHandler(
      IIssueTypeRepository issueTypeRepository,
      IMapper mapper,
      IUserContext userContext
    )
    {
      _issueTypeRepository = issueTypeRepository;
      _mapper = mapper;
      _userContext = userContext;
    }

    public async Task<ListModel<IssueTypeModel>> Handle(GetIssueTypesQuery request, CancellationToken cancellationToken)
    {
      PagedList<IssueType> issueTypes = await _issueTypeRepository.GetPagedAsync(
        _userContext.Id,
        request.Deleted,
        request.ProjectId,
        request.Search,
        request.Sort,
        request.Desc,
        request.Index,
        request.Count,
        readOnly: true,
        cancellationToken
      );

      return new ListModel<IssueTypeModel>(
        _mapper.Map<IEnumerable<IssueTypeModel>>(issueTypes),
        issueTypes.Total
      );
    }
  }
}
