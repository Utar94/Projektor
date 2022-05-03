namespace Projektor.Core.Issues.Payloads
{
  public class CreateIssuePayload : SaveIssuePayload
  {
    public Guid TypeId { get; set; }
  }
}
