using AutoMapper;
using Logitar.Identity.Core;
using MediatR;
using Projektor.Core.Repositories;
using Projektor.Core.Worklogs.Models;

namespace Projektor.Core.Worklogs.Queries
{
  internal class GetWorklogQueryHandler : IRequestHandler<GetWorklogQuery, WorklogModel>
  {
    private readonly IMapper _mapper;
    private readonly IUserContext _userContext;
    private readonly IWorklogRepository _worklogRepository;

    public GetWorklogQueryHandler(
      IMapper mapper,
      IUserContext userContext,
      IWorklogRepository worklogRepository
    )
    {
      _mapper = mapper;
      _userContext = userContext;
      _worklogRepository = worklogRepository;
    }

    public async Task<WorklogModel> Handle(GetWorklogQuery request, CancellationToken cancellationToken)
    {
      Worklog worklog = await _worklogRepository
        .GetAsync(request.Id, readOnly: true, cancellationToken)
        ?? throw new EntityNotFoundException<Worklog>(request.Id);
      
      if (worklog.CreatedById != _userContext.Id)
      {
        throw new UnauthorizedOperationException<Worklog>(worklog, _userContext.Id);
      }

      return _mapper.Map<WorklogModel>(worklog);
    }
  }
}
