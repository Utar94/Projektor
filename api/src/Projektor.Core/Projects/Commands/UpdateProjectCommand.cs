using MediatR;
using Projektor.Core.Projects.Models;
using Projektor.Core.Projects.Payloads;

namespace Projektor.Core.Projects.Commands
{
  public class UpdateProjectCommand : IRequest<ProjectModel>
  {
    public UpdateProjectCommand(Guid id, UpdateProjectPayload payload)
    {
      Id = id;
      Payload = payload ?? throw new ArgumentNullException(nameof(payload));
    }

    public Guid Id { get; }
    public UpdateProjectPayload Payload { get; }
  }
}
