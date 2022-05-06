using Projektor.Core.Issues;

namespace Projektor.Core.Worklogs
{
  public class Worklog : Aggregate
  {
    public Worklog(Issue issue, Guid userId) : base(userId)
    {
      Issue = issue ?? throw new ArgumentNullException(nameof(issue));
      IssueId = issue.Id;
    }
    private Worklog() : base()
    {
    }

    public string? Description { get; set; }
    public DateTime EndedAt { get; set; }
    public Issue? Issue { get; set; }
    public int IssueId { get; set; }
    public DateTime StartedAt { get; set; }
  }
}
