using Logitar.Validation;
using System.ComponentModel.DataAnnotations;

namespace Projektor.Core.Issues.Payloads
{
  public abstract class SaveIssuePayload
  {
    public string? Description { get; set; }

    public DateTime? DueDate { get; set; }

    [MinValue(0)]
    public int? Estimate { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [MinValue(0)]
    public double? Score { get; set; }
  }
}
