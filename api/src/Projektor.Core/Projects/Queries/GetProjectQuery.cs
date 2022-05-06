using MediatR;
using Projektor.Core.Projects.Models;

namespace Projektor.Core.Projects.Queries
{
  public class GetProjectQuery : IRequest<ProjectModel>
  {
    public GetProjectQuery(string id)
    {
      Id = id ?? throw new ArgumentNullException(nameof(id));
    }

    public string Id { get; }
  }
}
