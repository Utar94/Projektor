using Projektor.Core.Models;

namespace Projektor.Core.Projects.Models
{
  public class ProjectModel : AggregateModel
  {
    public string? Description { get; set; }
    public string Key { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
  }
}
