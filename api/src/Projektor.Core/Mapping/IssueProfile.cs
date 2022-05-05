using AutoMapper;
using Projektor.Core.Issues;
using Projektor.Core.Issues.Models;
using Projektor.Core.Models;
using Projektor.Core.Projects;

namespace Projektor.Core.Mapping
{
  internal class IssueProfile : Profile
  {
    public IssueProfile()
    {
      CreateMap<Issue, IssueModel>()
        .IncludeBase<Aggregate, AggregateModel>()
        .ForMember(x => x.Key, x => x.MapFrom(GetKey));
    }

    private static string GetKey(Issue issue, IssueModel model)
    {
      ArgumentNullException.ThrowIfNull(issue);

      Project project = issue.Project ?? throw new ArgumentException($"The {nameof(issue.Project)} is required.", nameof(issue));

      return $"{project.Key.ToUpperInvariant()}-{issue.Number}";
    }
  }
}
