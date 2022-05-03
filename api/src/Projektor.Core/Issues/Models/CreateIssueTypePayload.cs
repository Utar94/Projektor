namespace Projektor.Core.Issues.Models
{
  public class CreateIssueTypePayload : SaveIssueTypePayload
  {
    public Guid ProjectId { get; set; }
  }
}
