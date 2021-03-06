using System.ComponentModel.DataAnnotations;

namespace Projektor.Core.Projects.Payloads
{
  public abstract class SaveProjectPayload
  {
    public string? Description { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
  }
}
