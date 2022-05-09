namespace Projektor.Core.Comments.Payloads
{
  public class CreateCommentPayload : SaveCommentPayload
  {
    public Guid IssueId { get; set; }
  }
}
