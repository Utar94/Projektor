using Projektor.Core.Models;

namespace Projektor.Core.Issues.Models
{
  public class IssueTypeModel : AggregateModel
  {
    public string? Description { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid ProjectId { get; set; }
  }
}
