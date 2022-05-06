using MediatR;
using Projektor.Core.Issues.Models;
using Projektor.Core.Issues.Payloads;

namespace Projektor.Core.Issues.Commands
{
  public class UpdateIssueTypeCommand : IRequest<IssueTypeModel>
  {
    public UpdateIssueTypeCommand(Guid id, UpdateIssueTypePayload payload)
    {
      Id = id;
      Payload = payload ?? throw new ArgumentNullException(nameof(payload));
    }

    public Guid Id { get; }
    public UpdateIssueTypePayload Payload { get; }
  }
}
