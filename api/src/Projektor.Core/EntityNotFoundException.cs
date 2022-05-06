using Logitar.WebApiToolKit.Core.Exceptions;
using System.Text;

namespace Projektor.Core
{
  internal class EntityNotFoundException<T> : NotFoundException
  {
    public EntityNotFoundException(Guid id, string? paramName = null) : this(id.ToString(), paramName)
    {
    }
    public EntityNotFoundException(
      string id,
      string? paramName = null,
      string? message = null,
      Exception? innerException = null
    ) : base(paramName, message ?? GetMessage(id), innerException)
    {
      Id = id ?? throw new ArgumentNullException(nameof(id));
    }

    public string Id { get; }

    private static string GetMessage(string id)
    {
      var message = new StringBuilder();

      message.AppendLine("The entity could not be found.");
      message.AppendLine($"Type: {typeof(T).AssemblyQualifiedName}");
      message.AppendLine($"Id: {id}");

      return message.ToString();
    }
  }
}
