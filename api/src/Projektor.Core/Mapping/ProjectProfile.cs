using AutoMapper;
using Projektor.Core.Models;
using Projektor.Core.Projects;
using Projektor.Core.Projects.Models;

namespace Projektor.Core.Mapping
{
  internal class ProjectProfile : Profile
  {
    public ProjectProfile()
    {
      CreateMap<Project, ProjectModel>()
        .IncludeBase<Aggregate, AggregateModel>();
    }
  }
}
