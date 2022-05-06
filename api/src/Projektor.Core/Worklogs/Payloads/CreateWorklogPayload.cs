namespace Projektor.Core.Worklogs.Payloads
{
  public class CreateWorklogPayload : SaveWorklogPayload
  {
    public Guid IssueId { get; set; }
  }
}
