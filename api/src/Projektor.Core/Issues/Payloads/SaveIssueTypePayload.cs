using System.ComponentModel.DataAnnotations;

namespace Projektor.Core.Issues.Payloads
{
  public abstract class SaveIssueTypePayload
  {
    public string? Description { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
  }
}
