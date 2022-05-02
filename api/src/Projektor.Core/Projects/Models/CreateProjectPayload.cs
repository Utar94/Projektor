using Projektor.Core.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Projektor.Core.Projects.Models
{
  public class CreateProjectPayload : SaveProjectPayload
  {
    [Required]
    [StringLength(12)]
    [Alias]
    public string Alias { get; set; } = string.Empty;
  }
}
