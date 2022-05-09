using Projektor.Core.Models;

namespace Projektor.Core.Comments.Models
{
  public class CommentModel : AggregateModel
  {
    public string Text { get; set; } = string.Empty;
  }
}
