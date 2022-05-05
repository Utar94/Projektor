using AutoMapper;
using Logitar;
using Logitar.Identity.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projektor.Core.Issues;
using Projektor.Core.Issues.Models;
using Projektor.Core.Issues.Payloads;
using Projektor.Core.Models;
using Projektor.Core.Projects;
using Projektor.Infrastructure;

namespace Projektor.Web.Controllers
{
  [ApiController]
  [Authorize]
  [Route("issues/types")]
  public class IssueTypeController : ControllerBase
  {
    private readonly ProjektorDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IUserContext _userContext;

    public IssueTypeController(ProjektorDbContext dbContext, IMapper mapper, IUserContext userContext)
    {
      _dbContext = dbContext;
      _mapper = mapper;
      _userContext = userContext;
    }

    [HttpPost]
    public async Task<ActionResult<IssueTypeModel>> CreateAsync(
      [FromBody] CreateIssueTypePayload payload,
      CancellationToken cancellationToken
    )
    {
      Project? project = await _dbContext.Projects.SingleOrDefaultAsync(x => x.Uuid == payload.ProjectId, cancellationToken);
      if (project == null)
      {
        return NotFound(new { field = nameof(payload.ProjectId) });
      }
      else if (project.CreatedById != _userContext.Id)
      {
        return Forbid();
      }

      var issueType = new IssueType(project, _userContext.Id)
      {
        Description = payload.Description?.CleanTrim(),
        Name = payload.Name.Trim()
      };

      _dbContext.IssueTypes.Add(issueType);
      await _dbContext.SaveChangesAsync(cancellationToken);

      var model = _mapper.Map<IssueTypeModel>(issueType);
      var uri = new Uri($"/issues/types/{model.Id}", UriKind.Relative);

      return Created(uri, model);
    }

    [HttpGet]
    public async Task<ActionResult<ListModel<IssueTypeModel>>> GetAsync(
      bool? deleted,
      Guid? projectId,
      string? search,
      IssueTypeSort? sort,
      bool desc,
      int? index,
      int? count,
      CancellationToken cancellationToken
    )
    {
      Project? project = null;
      if (projectId.HasValue)
      {
        project = await _dbContext.Projects
          .AsNoTracking()
          .SingleOrDefaultAsync(x => x.Uuid == projectId.Value, cancellationToken);

        if (project == null)
        {
          return NotFound(new { field = nameof(projectId) });
        }
        else if (project.CreatedById != _userContext.Id)
        {
          return Forbid();
        }
      }

      IQueryable<IssueType> query = _dbContext.IssueTypes
        .AsNoTracking()
        .Include(x => x.Project)
        .Where(x => x.CreatedById == _userContext.Id);

      if (deleted.HasValue)
      {
        query = query.Where(x => x.Deleted == deleted);
      }
      if (project != null)
      {
        query = query.Where(x => x.Project != null && x.Project.Id == project.Id);
      }
      if (search != null)
      {
        query = query.Where(x => x.Name.Contains(search));
      }

      long total = await query.LongCountAsync(cancellationToken);

      if (sort.HasValue)
      {
        query = sort.Value switch
        {
          IssueTypeSort.Name => desc ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name),
          IssueTypeSort.Project => desc ? query.OrderByDescending(x => x.Project!.Name) : query.OrderBy(x => x.Project!.Name),
          IssueTypeSort.UpdatedAt => desc ? query.OrderByDescending(x => x.UpdatedAt ?? x.CreatedAt) : query.OrderBy(x => x.UpdatedAt ?? x.CreatedAt),
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

      IssueType[] issueTypes = await query.ToArrayAsync(cancellationToken);

      return Ok(new ListModel<IssueTypeModel>(
        _mapper.Map<IEnumerable<IssueTypeModel>>(issueTypes),
        total
      ));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IssueTypeModel>> GetAsync(Guid id, CancellationToken cancellationToken)
    {
      IssueType? issueType = await _dbContext.IssueTypes
        .AsNoTracking()
        .Include(x => x.Project)
        .SingleOrDefaultAsync(x => x.Uuid == id, cancellationToken);

      if (issueType == null)
      {
        return NotFound();
      }
      else if (issueType.CreatedById != _userContext.Id)
      {
        return Forbid();
      }

      return Ok(_mapper.Map<IssueTypeModel>(issueType));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<IssueTypeModel>> UpdateAsync(
      Guid id,
      [FromBody] UpdateIssueTypePayload payload,
      CancellationToken cancellationToken
    )
    {
      IssueType? issueType = await _dbContext.IssueTypes
        .Include(x => x.Project)
        .SingleOrDefaultAsync(x => x.Uuid == id, cancellationToken);
      if (issueType == null)
      {
        return NotFound();
      }
      else if (issueType.CreatedById != _userContext.Id)
      {
        return Forbid();
      }

      issueType.Description = payload.Description?.CleanTrim();
      issueType.Name = payload.Name.Trim();
      issueType.Update(_userContext.Id);

      await _dbContext.SaveChangesAsync(cancellationToken);

      return Ok(_mapper.Map<IssueTypeModel>(issueType));
    }
  }
}
