using AutoMapper;
using Projektor.Core.Issues;
using Projektor.Core.Issues.Models;
using Projektor.Core.Models;

namespace Projektor.Core.Mapping
{
  internal class IssueProfile : Profile
  {
    public IssueProfile()
    {
      CreateMap<Issue, IssueModel>()
        .IncludeBase<Aggregate, AggregateModel>()
        .ForMember(x => x.ProjectId, x => x.MapFrom(GetProjectId))
        .ForMember(x => x.TypeId, x => x.MapFrom(GetTypeId));
    }

    private static Guid GetProjectId(Issue issue, IssueModel model)
    {
      ArgumentNullException.ThrowIfNull(issue);

      return issue.Project?.Uuid
        ?? throw new ArgumentException($"The {nameof(issue.Project)} is required.", nameof(issue));
    }
    private static Guid GetTypeId(Issue issue, IssueModel model)
    {
      ArgumentNullException.ThrowIfNull(issue);

      return issue.Type?.Uuid
        ?? throw new ArgumentException($"The {nameof(issue.Type)} is required.", nameof(issue));
    }
  }
}
