using Projektor.Core.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Projektor.Core.Projects.Payloads
{
  public class CreateProjectPayload : SaveProjectPayload
  {
    [Required]
    [StringLength(12)]
    [ProjectKey]
    public string Key { get; set; } = string.Empty;
  }
}
