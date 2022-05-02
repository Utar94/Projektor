using AutoMapper;
using Projektor.Core.Models;

namespace Projektor.Core.Mapping
{
  internal class AggregateProfile : Profile
  {
    public AggregateProfile()
    {
      CreateMap<Aggregate, AggregateModel>()
        .ForMember(x => x.Id, x => x.MapFrom(y => y.Key));
    }
  }
}
