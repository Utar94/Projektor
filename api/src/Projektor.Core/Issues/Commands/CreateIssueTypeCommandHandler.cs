using AutoMapper;
using Logitar.Identity.Core;
using MediatR;
using Projektor.Core.Issues.Models;
using Projektor.Core.Projects;
using Projektor.Core.Repositories;

namespace Projektor.Core.Issues.Commands
{
  internal class CreateIssueTypeCommandHandler : SaveIssueTypeHandler, IRequestHandler<CreateIssueTypeCommand, IssueTypeModel>
  {
    private readonly IProjectRepository _projectRepository;
    private readonly IUserContext _userContext;

    public CreateIssueTypeCommandHandler(
      IMapper mapper,
      IIssueTypeRepository issueTypeRepository,
      IProjectRepository projectRepository,
      IUserContext userContext
    ) : base(issueTypeRepository, mapper)
    {
      _projectRepository = projectRepository;
      _userContext = userContext;
    }

    public async Task<IssueTypeModel> Handle(CreateIssueTypeCommand request, CancellationToken cancellationToken)
    {
      Project project = await _projectRepository
        .GetAsync(request.Payload.ProjectId, readOnly: false, cancellationToken)
        ?? throw new EntityNotFoundException<Project>(request.Payload.ProjectId, nameof(request.Payload.ProjectId));

      if (project.CreatedById != _userContext.Id)
      {
        throw new UnauthorizedOperationException<Project>(project, _userContext.Id);
      }

      var issueType = new IssueType(project, _userContext.Id);

      return await SaveAsync(issueType, request.Payload, cancellationToken);
    }
  }
}
