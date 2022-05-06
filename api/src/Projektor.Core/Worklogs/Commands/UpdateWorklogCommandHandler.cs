using AutoMapper;
using Logitar.Identity.Core;
using MediatR;
using Projektor.Core.Repositories;
using Projektor.Core.Worklogs.Models;

namespace Projektor.Core.Worklogs.Commands
{
  internal class UpdateWorklogCommandHandler : SaveWorklogHandler, IRequestHandler<UpdateWorklogCommand, WorklogModel>
  {
    private readonly IUserContext _userContext;
    private readonly IWorklogRepository _worklogRepository;

    public UpdateWorklogCommandHandler(
      IMapper mapper,
      IUserContext userContext,
      IWorklogRepository worklogRepository
    ) : base(mapper, worklogRepository)
    {
      _userContext = userContext;
      _worklogRepository = worklogRepository;
    }

    public async Task<WorklogModel> Handle(UpdateWorklogCommand request, CancellationToken cancellationToken)
    {
      Worklog worklog = await _worklogRepository
        .GetAsync(request.Id, readOnly: false, cancellationToken)
        ?? throw new EntityNotFoundException<Worklog>(request.Id);

      if (worklog.CreatedById != _userContext.Id)
      {
        throw new UnauthorizedOperationException<Worklog>(worklog, _userContext.Id);
      }

      worklog.Update(_userContext.Id);

      return await SaveAsync(worklog, request.Payload, cancellationToken);
    }
  }
}
