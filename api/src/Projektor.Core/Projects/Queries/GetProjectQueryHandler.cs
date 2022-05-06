using AutoMapper;
using Logitar.Identity.Core;
using MediatR;
using Projektor.Core.Projects.Models;
using Projektor.Core.Repositories;

namespace Projektor.Core.Projects.Queries
{
  internal class GetProjectQueryHandler : IRequestHandler<GetProjectQuery, ProjectModel>
  {
    private readonly IMapper _mapper;
    private readonly IProjectRepository _projectRepository;
    private readonly IUserContext _userContext;

    public GetProjectQueryHandler(
      IMapper mapper,
      IProjectRepository projectRepository,
      IUserContext userContext
    )
    {
      _mapper = mapper;
      _projectRepository = projectRepository;
      _userContext = userContext;
    }

    public async Task<ProjectModel> Handle(GetProjectQuery request, CancellationToken cancellationToken)
    {
      Project project = (Guid.TryParse(request.Id, out Guid uuid)
        ? await _projectRepository.GetAsync(uuid, readOnly: true, cancellationToken)
        : await _projectRepository.GetAsync(request.Id, readOnly: true, cancellationToken)
      ) ?? throw new EntityNotFoundException<Project>(request.Id);

      if (project.CreatedById != _userContext.Id)
      {
        throw new UnauthorizedOperationException<Project>(project, _userContext.Id);
      }

      return _mapper.Map<ProjectModel>(project);
    }
  }
}
