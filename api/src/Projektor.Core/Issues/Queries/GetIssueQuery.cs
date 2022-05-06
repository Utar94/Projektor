using MediatR;
using Projektor.Core.Issues.Models;

namespace Projektor.Core.Issues.Queries
{
  public class GetIssueQuery : IRequest<IssueModel>
  {
    public GetIssueQuery(string id)
    {
      Id = id ?? throw new ArgumentNullException(nameof(id));
    }

    public string Id { get; }
  }
}
