using AutoMapper;
using Logitar.Identity.Core;
using MediatR;
using Projektor.Core.Models;
using Projektor.Core.Projects.Models;
using Projektor.Core.Repositories;

namespace Projektor.Core.Projects.Queries
{
  internal class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, ListModel<ProjectModel>>
  {
    private readonly IMapper _mapper;
    private readonly IProjectRepository _projectRepository;
    private readonly IUserContext _userContext;

    public GetProjectsQueryHandler(
      IMapper mapper,
      IProjectRepository projectRepository,
      IUserContext userContext
    )
    {
      _mapper = mapper;
      _projectRepository = projectRepository;
      _userContext = userContext;
    }

    public async Task<ListModel<ProjectModel>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
      PagedList<Project> projects = await _projectRepository.GetPagedAsync(
        _userContext.Id,
        request.Deleted,
        request.Search,
        request.Sort,
        request.Desc,
        request.Index,
        request.Count,
        readOnly: true,
        cancellationToken
      );

      return new ListModel<ProjectModel>(
        _mapper.Map<IEnumerable<ProjectModel>>(projects),
        projects.Total
      );
    }
  }
}
