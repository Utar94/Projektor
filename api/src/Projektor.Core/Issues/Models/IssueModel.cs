using Projektor.Core.Models;
using Projektor.Core.Projects.Models;

namespace Projektor.Core.Issues.Models
{
  public class IssueModel : AggregateModel
  {
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public int? Estimate { get; set; }
    public string Key { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Number { get; set; }
    public Priority Priority { get; set; }
    public ProjectModel? Project { get; set; }
    public double? Score { get; set; }
    public IssueTypeModel? Type { get; set; }
  }
}
