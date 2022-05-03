using Projektor.Core.Models;

namespace Projektor.Core.Issues.Models
{
  public class IssueModel : AggregateModel
  {
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public int? Estimate { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Number { get; set; }
    public Guid ProjectId { get; set; }
    public double? Score { get; set; }
    public Guid TypeId { get; set; }
  }
}
