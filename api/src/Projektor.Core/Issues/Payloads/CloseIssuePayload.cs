using Logitar.Validation;

namespace Projektor.Core.Issues.Payloads
{
  public class CloseIssuePayload
  {
    [Enum(typeof(Resolution))]
    public Resolution Resolution { get; set; }
  }
}
