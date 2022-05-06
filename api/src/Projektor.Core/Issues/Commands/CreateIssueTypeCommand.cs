using MediatR;
using Projektor.Core.Issues.Models;
using Projektor.Core.Issues.Payloads;

namespace Projektor.Core.Issues.Commands
{
  public class CreateIssueTypeCommand : IRequest<IssueTypeModel>
  {
    public CreateIssueTypeCommand(CreateIssueTypePayload payload)
    {
      Payload = payload ?? throw new ArgumentNullException(nameof(payload));
    }

    public CreateIssueTypePayload Payload { get; }
  }
}
