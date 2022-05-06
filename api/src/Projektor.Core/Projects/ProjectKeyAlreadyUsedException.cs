using Logitar.WebApiToolKit.Core.Exceptions;

namespace Projektor.Core.Projects
{
  internal class ProjectKeyAlreadyUsedException : ConflictException
  {
    public ProjectKeyAlreadyUsedException(
      string key,
      string? paramName = null,
      string? message = null,
      Exception? innerException = null
    ) : base(paramName, message ?? $"The project key \"{key}\" is already used.", innerException)
    {
      Key = key ?? throw new ArgumentNullException(nameof(key));
    }

    public string Key { get; }
  }
}
