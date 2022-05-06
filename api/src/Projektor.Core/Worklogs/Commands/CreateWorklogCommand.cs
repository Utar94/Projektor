using MediatR;
using Projektor.Core.Worklogs.Models;
using Projektor.Core.Worklogs.Payloads;

namespace Projektor.Core.Worklogs.Commands
{
  public class CreateWorklogCommand : IRequest<WorklogModel>
  {
    public CreateWorklogCommand(CreateWorklogPayload payload)
    {
      Payload = payload ?? throw new ArgumentNullException(nameof(payload));
    }

    public CreateWorklogPayload Payload { get; }
  }
}
