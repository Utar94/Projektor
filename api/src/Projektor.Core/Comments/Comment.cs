using Projektor.Core.Issues;

namespace Projektor.Core.Comments
{
  public class Comment : Aggregate
  {
    public Comment(Issue issue, Guid userId) : base(userId)
    {
      Issue = issue ?? throw new ArgumentNullException(nameof(issue));
      IssueId = issue.Id;
    }
    private Comment() : base()
    {
    }

    public Issue? Issue { get; set; }
    public int IssueId { get; set; }
    public string Text { get; set; } = string.Empty;
  }
}
