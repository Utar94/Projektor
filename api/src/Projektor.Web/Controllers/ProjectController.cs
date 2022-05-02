using AutoMapper;
using Logitar;
using Logitar.Identity.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projektor.Core.Models;
using Projektor.Core.Projects;
using Projektor.Core.Projects.Models;
using Projektor.Infrastructure;

namespace Projektor.Web.Controllers
{
  [ApiController]
  [Authorize]
  [Route("projects")]
  public class ProjectController : ControllerBase
  {
    private readonly ProjektorDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IUserContext _userContext;

    public ProjectController(ProjektorDbContext dbContext, IMapper mapper, IUserContext userContext)
    {
      _dbContext = dbContext;
      _mapper = mapper;
      _userContext = userContext;
    }

    [HttpPost]
    public async Task<ActionResult<ProjectModel>> CreateAsync(
      [FromBody] CreateProjectPayload payload,
      CancellationToken cancellationToken
    )
    {
      string alias = payload.Alias.ToLowerInvariant();
      if (await _dbContext.Projects.SingleOrDefaultAsync(x => x.Alias == alias, cancellationToken) != null)
      {
        return Conflict(new { field = nameof(payload.Alias) });
      }

      var project = new Project(alias, _userContext.Id)
      {
        Description = payload.Description?.CleanTrim(),
        Name = payload.Name.Trim()
      };

      _dbContext.Projects.Add(project);
      await _dbContext.SaveChangesAsync(cancellationToken);

      var model = _mapper.Map<ProjectModel>(project);
      var uri = new Uri($"/projects/{model.Id}", UriKind.Relative);

      return Created(uri, model);
    }

    [HttpGet]
    public async Task<ActionResult<ListModel<ProjectModel>>> GetAsync(
      bool? deleted,
      string? search,
      ProjectSort? sort,
      bool desc,
      int? index,
      int? count,
      CancellationToken cancellationToken
    )
    {
      IQueryable<Project> query = _dbContext.Projects
        .AsNoTracking()
        .Where(x => x.CreatedById == _userContext.Id);

      if (deleted.HasValue)
      {
        query = query.Where(x => x.Deleted == deleted);
      }
      if (search != null)
      {
        query = query.Where(x => x.Alias.Contains(search) || x.Name.Contains(search));
      }

      long total = await query.LongCountAsync(cancellationToken);

      if (sort.HasValue)
      {
        query = sort.Value switch
        {
          ProjectSort.Alias => desc ? query.OrderByDescending(x => x.Alias) : query.OrderBy(x => x.Alias),
          ProjectSort.Name => desc ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name),
          ProjectSort.UpdatedAt => desc ? query.OrderByDescending(x => x.UpdatedAt ?? x.CreatedAt) : query.OrderBy(x => x.UpdatedAt ?? x.CreatedAt),
          _ => throw new ArgumentException($"The sort \"{sort}\" is not valid.", nameof(sort)),
        };
      }

      if (index.HasValue)
      {
        query = query.Skip(index.Value);
      }
      if (count.HasValue)
      {
        query = query.Take(count.Value);
      }

      Project[] projects = await query.ToArrayAsync(cancellationToken);

      return Ok(new ListModel<ProjectModel>(
        _mapper.Map<IEnumerable<ProjectModel>>(projects),
        total
      ));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectModel>> GetAsync(Guid id, CancellationToken cancellationToken)
    {
      Project? project = await _dbContext.Projects
        .AsNoTracking()
        .SingleOrDefaultAsync(x => x.Key == id, cancellationToken);

      if (project == null)
      {
        return NotFound();
      }
      else if (project.CreatedById != _userContext.Id)
      {
        return Forbid();
      }

      return Ok(_mapper.Map<ProjectModel>(project));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectModel>> UpdateAsync(
      Guid id,
      [FromBody] UpdateProjectPayload payload,
      CancellationToken cancellationToken
    )
    {
      Project? project = await _dbContext.Projects.SingleOrDefaultAsync(x => x.Key == id, cancellationToken);
      if (project == null)
      {
        return NotFound();
      }
      else if (project.CreatedById != _userContext.Id)
      {
        return Forbid();
      }

      project.Description = payload.Description?.CleanTrim();
      project.Name = payload.Name.Trim();
      project.Update(_userContext.Id);

      await _dbContext.SaveChangesAsync(cancellationToken);

      return Ok(_mapper.Map<ProjectModel>(project));
    }
  }
}
