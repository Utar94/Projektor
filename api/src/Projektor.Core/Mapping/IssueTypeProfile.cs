using AutoMapper;
using Projektor.Core.Issues;
using Projektor.Core.Issues.Models;
using Projektor.Core.Models;

namespace Projektor.Core.Mapping
{
  internal class IssueTypeProfile : Profile
  {
    public IssueTypeProfile()
    {
      CreateMap<IssueType, IssueTypeModel>()
        .IncludeBase<Aggregate, AggregateModel>()
        .ForMember(x => x.ProjectId, x => x.MapFrom(GetProjectId));
    }

    private static Guid GetProjectId(IssueType issueType, IssueTypeModel model)
    {
      ArgumentNullException.ThrowIfNull(issueType);

      return issueType.Project?.Uuid
        ?? throw new ArgumentException($"The {nameof(issueType.Project)} is required.", nameof(issueType));
    }
  }
}
