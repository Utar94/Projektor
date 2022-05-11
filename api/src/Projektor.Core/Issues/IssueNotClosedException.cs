using Logitar.WebApiToolKit.Core.Exceptions;

namespace Projektor.Core.Issues
{
  public class IssueNotClosedException : BadRequestException
  {
    public IssueNotClosedException(
      Issue issue,
      string? code = null,
      string? message = null,
      Exception? innerException = null
    ) : base(code ?? " IssueNotClosed", message ?? "The issue is not closed.", innerException)
    {
      Issue = issue ?? throw new ArgumentNullException(nameof(issue));
    }

    public Issue Issue { get; }
  }
}
