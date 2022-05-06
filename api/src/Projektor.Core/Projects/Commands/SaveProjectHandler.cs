using AutoMapper;
using Logitar;
using Projektor.Core.Projects.Models;
using Projektor.Core.Projects.Payloads;
using Projektor.Core.Repositories;

namespace Projektor.Core.Projects.Commands
{
  internal abstract class SaveProjectHandler
  {
    private readonly IMapper _mapper;
    private readonly IProjectRepository _projectRepository;

    protected SaveProjectHandler(IMapper mapper, IProjectRepository projectRepository)
    {
      _mapper = mapper;
      _projectRepository = projectRepository;
    }

    protected async Task<ProjectModel> SaveAsync(Project project, SaveProjectPayload payload, CancellationToken cancellationToken = default)
    {
      project.Description = payload.Description?.CleanTrim();
      project.Name = payload.Name.Trim();

      await _projectRepository.SaveAsync(project, cancellationToken);

      return _mapper.Map<ProjectModel>(project);
    }
  }
}
