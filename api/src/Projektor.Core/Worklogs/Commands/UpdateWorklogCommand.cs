using MediatR;
using Projektor.Core.Worklogs.Models;
using Projektor.Core.Worklogs.Payloads;

namespace Projektor.Core.Worklogs.Commands
{
  public class UpdateWorklogCommand : IRequest<WorklogModel>
  {
    public UpdateWorklogCommand(Guid id, UpdateWorklogPayload payload)
    {
      Id = id;
      Payload = payload ?? throw new ArgumentNullException(nameof(payload));
    }

    public Guid Id { get; }
    public UpdateWorklogPayload Payload { get; }
  }
}
