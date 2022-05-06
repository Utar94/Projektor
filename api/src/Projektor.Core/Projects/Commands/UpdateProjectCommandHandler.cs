using AutoMapper;
using Logitar.Identity.Core;
using MediatR;
using Projektor.Core.Projects.Models;
using Projektor.Core.Repositories;

namespace Projektor.Core.Projects.Commands
{
  internal class UpdateProjectCommandHandler : SaveProjectHandler, IRequestHandler<UpdateProjectCommand, ProjectModel>
  {
    private readonly IProjectRepository _projectRepository;
    private readonly IUserContext _userContext;

    public UpdateProjectCommandHandler(
      IMapper mapper,
      IProjectRepository projectRepository,
      IUserContext userContext
    ) : base(mapper, projectRepository)
    {
      _projectRepository = projectRepository;
      _userContext = userContext;
    }

    public async Task<ProjectModel> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
      Project project = await _projectRepository
        .GetAsync(request.Id, readOnly: false, cancellationToken)
        ?? throw new EntityNotFoundException<Project>(request.Id);
      
      if (project.CreatedById != _userContext.Id)
      {
        throw new UnauthorizedOperationException<Project>(project, _userContext.Id);
      }

      project.Update(_userContext.Id);

      return await SaveAsync(project, request.Payload, cancellationToken);
    }
  }
}
