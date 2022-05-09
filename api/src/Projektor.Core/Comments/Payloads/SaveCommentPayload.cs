using System.ComponentModel.DataAnnotations;

namespace Projektor.Core.Comments.Payloads
{
  public abstract class SaveCommentPayload
  {
    [Required]
    public string Text { get; set; } = string.Empty;
  }
}
