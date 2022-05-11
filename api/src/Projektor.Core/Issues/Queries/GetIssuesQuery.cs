using MediatR;
using Projektor.Core.Models;
using Projektor.Core.Issues.Models;

namespace Projektor.Core.Issues.Queries
{
  public class GetIssuesQuery : IRequest<ListModel<IssueModel>>
  {
    public bool? Deleted { get; set; }
    public Priority? Priority { get; set; }
    public Guid? ProjectId { get; set; }
    public string? Search { get; set; }
    public Guid? TypeId { get; set; }

    public IssueSort? Sort { get; set; }
    public bool Desc { get; set; }

    public int? Index { get; set; }
    public int? Count { get; set; }
  }
}
