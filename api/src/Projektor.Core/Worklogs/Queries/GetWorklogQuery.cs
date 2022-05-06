using MediatR;
using Projektor.Core.Worklogs.Models;

namespace Projektor.Core.Worklogs.Queries
{
  public class GetWorklogQuery : IRequest<WorklogModel>
  {
    public GetWorklogQuery(Guid id)
    {
      Id = id;
    }

    public Guid Id { get; }
  }
}
