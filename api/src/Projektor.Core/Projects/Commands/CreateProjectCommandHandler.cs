using AutoMapper;
using Logitar.Identity.Core;
using MediatR;
using Projektor.Core.Projects.Models;
using Projektor.Core.Repositories;

namespace Projektor.Core.Projects.Commands
{
  internal class CreateProjectCommandHandler : SaveProjectHandler, IRequestHandler<CreateProjectCommand, ProjectModel>
  {
    private readonly IProjectRepository _projectRepository;
    private readonly IUserContext _userContext;

    public CreateProjectCommandHandler(
      IMapper mapper,
      IProjectRepository projectRepository,
      IUserContext userContext
    ) : base(mapper, projectRepository)
    {
      _projectRepository = projectRepository;
      _userContext = userContext;
    }

    public async Task<ProjectModel> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
      if (await _projectRepository.GetAsync(request.Payload.Key, readOnly: true, cancellationToken) != null)
      {
        throw new ProjectKeyAlreadyUsedException(request.Payload.Key, nameof(request.Payload.Key));
      }

      var project = new Project(request.Payload.Key, _userContext.Id);

      return await SaveAsync(project, request.Payload, cancellationToken);
    }
  }
}
