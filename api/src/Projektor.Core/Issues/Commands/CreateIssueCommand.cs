using MediatR;
using Projektor.Core.Issues.Models;
using Projektor.Core.Issues.Payloads;

namespace Projektor.Core.Issues.Commands
{
  public class CreateIssueCommand : IRequest<IssueModel>
  {
    public CreateIssueCommand(CreateIssuePayload payload)
    {
      Payload = payload ?? throw new ArgumentNullException(nameof(payload));
    }

    public CreateIssuePayload Payload { get; }
  }
}
