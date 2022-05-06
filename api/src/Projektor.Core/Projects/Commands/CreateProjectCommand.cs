using MediatR;
using Projektor.Core.Projects.Models;
using Projektor.Core.Projects.Payloads;

namespace Projektor.Core.Projects.Commands
{
  public class CreateProjectCommand : IRequest<ProjectModel>
  {
    public CreateProjectCommand(CreateProjectPayload payload)
    {
      Payload = payload ?? throw new ArgumentNullException(nameof(payload));
    }

    public CreateProjectPayload Payload { get; }
  }
}
