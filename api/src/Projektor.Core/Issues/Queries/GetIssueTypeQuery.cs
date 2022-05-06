using MediatR;
using Projektor.Core.Issues.Models;

namespace Projektor.Core.Issues.Queries
{
  public class GetIssueTypeQuery : IRequest<IssueTypeModel>
  {
    public GetIssueTypeQuery(Guid id)
    {
      Id = id;
    }

    public Guid Id { get; }
  }
}
