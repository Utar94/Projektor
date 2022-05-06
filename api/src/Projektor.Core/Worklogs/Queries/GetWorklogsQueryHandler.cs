using AutoMapper;
using Logitar.Identity.Core;
using MediatR;
using Projektor.Core.Models;
using Projektor.Core.Repositories;
using Projektor.Core.Worklogs.Models;

namespace Projektor.Core.Worklogs.Queries
{
  internal class GetWorklogsQueryHandler : IRequestHandler<GetWorklogsQuery, ListModel<WorklogModel>>
  {
    private readonly IMapper _mapper;
    private readonly IUserContext _userContext;
    private readonly IWorklogRepository _worklogRepository;

    public GetWorklogsQueryHandler(
      IMapper mapper,
      IUserContext userContext,
      IWorklogRepository worklogRepository
    )
    {
      _mapper = mapper;
      _userContext = userContext;
      _worklogRepository = worklogRepository;
    }

    public async Task<ListModel<WorklogModel>> Handle(GetWorklogsQuery request, CancellationToken cancellationToken)
    {
      PagedList<Worklog> worklogs = await _worklogRepository.GetPagedAsync(
        _userContext.Id,
        request.Deleted,
        request.IssueId,
        request.Sort,
        request.Desc,
        request.Index,
        request.Count,
        readOnly: true,
        cancellationToken
      );

      return new ListModel<WorklogModel>(
        _mapper.Map<IEnumerable<WorklogModel>>(worklogs),
        worklogs.Total
      );
    }
  }
}
