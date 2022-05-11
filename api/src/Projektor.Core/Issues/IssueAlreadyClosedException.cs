using Logitar.WebApiToolKit.Core.Exceptions;

namespace Projektor.Core.Issues
{
  public class IssueAlreadyClosedException : BadRequestException
  {
    public IssueAlreadyClosedException(
      Issue issue,
      string? code = null,
      string? message = null,
      Exception? innerException = null
    ) : base(code ?? "IssueAlreadyClosed", message ?? "The issue is already closed.", innerException)
    {
      Issue = issue ?? throw new ArgumentNullException(nameof(issue));
    }

    public Issue Issue { get; }
  }
}
