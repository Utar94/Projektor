using Logitar.WebApiToolKit.Core.Exceptions;

namespace Projektor.Core.Issues
{
  internal class InvalidIssueKeyException : BadRequestException
  {
    public InvalidIssueKeyException(
      string issueKey,
      string? code = null,
      string? message = null,
      Exception? innerException = null
    ) : base(code ?? "InvalidIssueKey", message ?? $"The issue key \"{issueKey}\" is not valid.", innerException)
    {
      IssueKey = issueKey ?? throw new ArgumentNullException(nameof(issueKey));
    }

    public string IssueKey { get; }
  }
}
