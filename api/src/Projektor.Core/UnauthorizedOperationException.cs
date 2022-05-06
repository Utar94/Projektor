using Logitar.WebApiToolKit.Core.Exceptions;
using System.Text;

namespace Projektor.Core
{
  internal class UnauthorizedOperationException<T> : ForbiddenException
  {
    public UnauthorizedOperationException(
      T entity,
      Guid userId,
      string? message = null,
      Exception? innerException = null
    ) : base(message ?? GetMessage(entity, userId), innerException)
    {
      Entity = entity ?? throw new ArgumentNullException(nameof(entity));
      UserId = userId;
    }

    public T Entity { get; }
    public Guid UserId { get; }

    private static string GetMessage(T entity, Guid userId)
    {
      var message = new StringBuilder();

      message.AppendLine("The entity could not be found.");
      message.AppendLine($"Entity: {entity}");
      message.AppendLine($"UserId: {userId}");

      return message.ToString();
    }
  }
}
