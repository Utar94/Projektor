using MediatR;
using Projektor.Core.Issues.Models;
using Projektor.Core.Issues.Payloads;

namespace Projektor.Core.Issues.Commands
{
  public class CloseIssueCommand : IRequest<IssueModel>
  {
    public CloseIssueCommand(Guid id, CloseIssuePayload payload)
    {
      Id = id;
      Payload = payload ?? throw new ArgumentNullException(nameof(payload));
    }

    public Guid Id { get; }
    public CloseIssuePayload Payload { get; }
  }
}
