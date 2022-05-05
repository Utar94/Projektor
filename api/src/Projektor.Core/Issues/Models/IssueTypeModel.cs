using Projektor.Core.Models;
using Projektor.Core.Projects.Models;

namespace Projektor.Core.Issues.Models
{
  public class IssueTypeModel : AggregateModel
  {
    public string? Description { get; set; }
    public string Name { get; set; } = string.Empty;
    public ProjectModel? Project { get; set; }
  }
}
