using AutoMapper;
using Projektor.Core.Models;
using Projektor.Core.Worklogs;
using Projektor.Core.Worklogs.Models;

namespace Projektor.Core.Mapping
{
  internal class WorklogProfile : Profile
  {
    public WorklogProfile()
    {
      CreateMap<Worklog, WorklogModel>()
        .IncludeBase<Aggregate, AggregateModel>();
    }
  }
}
