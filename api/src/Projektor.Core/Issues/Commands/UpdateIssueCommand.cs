using MediatR;
using Projektor.Core.Issues.Models;
using Projektor.Core.Issues.Payloads;

namespace Projektor.Core.Issues.Commands
{
  public class UpdateIssueCommand : IRequest<IssueModel>
  {
    public UpdateIssueCommand(Guid id, UpdateIssuePayload payload)
    {
      Id = id;
      Payload = payload ?? throw new ArgumentNullException(nameof(payload));
    }

    public Guid Id { get; }
    public UpdateIssuePayload Payload { get; }
  }
}
