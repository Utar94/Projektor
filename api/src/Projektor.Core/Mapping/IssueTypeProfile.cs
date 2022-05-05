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
        .IncludeBase<Aggregate, AggregateModel>();
    }
  }
}
