using MediatR;
using Projektor.Core.Issues.Models;

namespace Projektor.Core.Issues.Commands
{
  public class ReopenIssueCommand : IRequest<IssueModel>
  {
    public ReopenIssueCommand(Guid id)
    {
      Id = id;
    }

    public Guid Id { get; }
  }
}
