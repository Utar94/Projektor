using MediatR;
using Projektor.Core.Models;
using Projektor.Core.Projects.Models;

namespace Projektor.Core.Projects.Queries
{
  public class GetProjectsQuery : IRequest<ListModel<ProjectModel>>
  {
    public bool? Deleted { get; set; }
    public string? Search { get; set; }

    public ProjectSort? Sort { get; set; }
    public bool Desc { get; set; }

    public int? Index { get; set; }
    public int? Count { get; set; }
  }
}
