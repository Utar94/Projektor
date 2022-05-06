using AutoMapper;
using Logitar;
using Projektor.Core.Repositories;
using Projektor.Core.Worklogs.Models;
using Projektor.Core.Worklogs.Payloads;

namespace Projektor.Core.Worklogs.Commands
{
  internal abstract class SaveWorklogHandler
  {
    private readonly IMapper _mapper;
    private readonly IWorklogRepository _worklogRepository;

    protected SaveWorklogHandler(IMapper mapper, IWorklogRepository worklogRepository)
    {
      _mapper = mapper;
      _worklogRepository = worklogRepository;
    }

    protected async Task<WorklogModel> SaveAsync(Worklog worklog, SaveWorklogPayload payload, CancellationToken cancellationToken = default)
    {
      worklog.Description = payload.Description?.CleanTrim();
      worklog.EndedAt = payload.EndedAt;
      worklog.StartedAt = payload.StartedAt;

      await _worklogRepository.SaveAsync(worklog, cancellationToken);

      return _mapper.Map<WorklogModel>(worklog);
    }
  }
}
