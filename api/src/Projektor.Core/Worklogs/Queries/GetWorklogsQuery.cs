using MediatR;
using Projektor.Core.Models;
using Projektor.Core.Worklogs.Models;

namespace Projektor.Core.Worklogs.Queries
{
  public class GetWorklogsQuery : IRequest<ListModel<WorklogModel>>
  {
    public bool? Deleted { get; set; }
    public Guid? IssueId { get; set; }

    public WorklogSort? Sort { get; set; }
    public bool Desc { get; set; }

    public int? Index { get; set; }
    public int? Count { get; set; }
  }
}
