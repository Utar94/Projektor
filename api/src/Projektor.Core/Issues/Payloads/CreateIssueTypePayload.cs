namespace Projektor.Core.Issues.Payloads
{
  public class CreateIssueTypePayload : SaveIssueTypePayload
  {
    public Guid ProjectId { get; set; }
  }
}
