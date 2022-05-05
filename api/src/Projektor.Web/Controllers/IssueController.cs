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
  [Route("issues")]
  public class IssueController : ControllerBase
  {
    private readonly ProjektorDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IUserContext _userContext;

    public IssueController(ProjektorDbContext dbContext, IMapper mapper, IUserContext userContext)
    {
      _dbContext = dbContext;
      _mapper = mapper;
      _userContext = userContext;
    }

    [HttpPost]
    public async Task<ActionResult<IssueModel>> CreateAsync(
      [FromBody] CreateIssuePayload payload,
      CancellationToken cancellationToken
    )
    {
      IssueType? type = await _dbContext.IssueTypes
        .Include(x => x.Project)
        .SingleOrDefaultAsync(x => x.Uuid == payload.TypeId, cancellationToken);
      if (type == null)
      {
        return NotFound(new { field = nameof(payload.TypeId) });
      }
      else if (type.CreatedById != _userContext.Id)
      {
        return Forbid();
      }

      int number = ((await _dbContext.Issues
        .Where(x => x.ProjectId == type.ProjectId)
        .MaxAsync(x => (int?)x.Number, cancellationToken)) ?? 0) + 1;

      var issueType = new Issue(number, type, _userContext.Id)
      {
        Description = payload.Description?.CleanTrim(),
        DueDate = payload.DueDate,
        Estimate = payload.Estimate,
        Name = payload.Name.Trim(),
        Score = payload.Score
      };

      _dbContext.Issues.Add(issueType);
      await _dbContext.SaveChangesAsync(cancellationToken);

      var model = _mapper.Map<IssueModel>(issueType);
      var uri = new Uri($"/issues/{model.Id}", UriKind.Relative);

      return Created(uri, model);
    }

    [HttpGet]
    public async Task<ActionResult<ListModel<IssueModel>>> GetAsync(
      bool? deleted,
      Guid? projectId,
      string? search,
      Guid? typeId,
      IssueSort? sort,
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

      IssueType? type = null;
      if (typeId.HasValue)
      {
        type = await _dbContext.IssueTypes
          .AsNoTracking()
          .SingleOrDefaultAsync(x => x.Uuid == typeId.Value, cancellationToken);

        if (type == null)
        {
          return NotFound(new { field = nameof(typeId) });
        }
        else if (type.CreatedById != _userContext.Id)
        {
          return Forbid();
        }
      }

      IQueryable<Issue> query = _dbContext.Issues
        .AsNoTracking()
        .Include(x => x.Project)
        .Include(x => x.Type)
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
      if (type != null)
      {
        query = query.Where(x => x.Type != null && x.Type.Id == type.Id);
      }

      long total = await query.LongCountAsync(cancellationToken);

      if (sort.HasValue)
      {
        query = sort.Value switch
        {
          IssueSort.Key => desc
            ? query.OrderByDescending(x => x.Project!.Key).ThenByDescending(x => x.Number)
            : query.OrderBy(x => x.Project!.Key).ThenBy(x => x.Number),
          IssueSort.Name => desc ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name),
          IssueSort.Type => desc ? query.OrderByDescending(x => x.Type!.Name) : query.OrderBy(x => x.Type!.Name),
          IssueSort.UpdatedAt => desc ? query.OrderByDescending(x => x.UpdatedAt ?? x.CreatedAt) : query.OrderBy(x => x.UpdatedAt ?? x.CreatedAt),
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

      Issue[] issueTypes = await query.ToArrayAsync(cancellationToken);

      return Ok(new ListModel<IssueModel>(
        _mapper.Map<IEnumerable<IssueModel>>(issueTypes),
        total
      ));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IssueModel>> GetAsync(string id, CancellationToken cancellationToken)
    {
      IQueryable<Issue> query = _dbContext.Issues
        .AsNoTracking()
        .Include(x => x.Project)
        .Include(x => x.Type);

      Issue? issue;
      if (Guid.TryParse(id, out Guid uuid))
      {
        issue = await query.SingleOrDefaultAsync(x => x.Uuid == uuid, cancellationToken);
      }
      else
      {
        string[] values = id.Split('-');
        if (values.Length != 2 || !int.TryParse(values[1], out int number))
        {
          return BadRequest(new { code = "invalid_key" });
        }

        issue = await query.SingleOrDefaultAsync(x
          => x.Project != null && x.Project.Key == values[0].ToLowerInvariant()
          && x.Number == number, cancellationToken
        );
      }

      if (issue == null)
      {
        return NotFound();
      }
      else if (issue.CreatedById != _userContext.Id)
      {
        return Forbid();
      }

      return Ok(_mapper.Map<IssueModel>(issue));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<IssueModel>> UpdateAsync(
      Guid id,
      [FromBody] UpdateIssuePayload payload,
      CancellationToken cancellationToken
    )
    {
      Issue? issue = await _dbContext.Issues
        .Include(x => x.Project)
        .Include(x => x.Type)
        .SingleOrDefaultAsync(x => x.Uuid == id, cancellationToken);
      if (issue == null)
      {
        return NotFound();
      }
      else if (issue.CreatedById != _userContext.Id)
      {
        return Forbid();
      }

      issue.Description = payload.Description?.CleanTrim();
      issue.DueDate = payload.DueDate;
      issue.Estimate = payload.Estimate;
      issue.Name = payload.Name.Trim();
      issue.Score = payload.Score;
      issue.Update(_userContext.Id);

      await _dbContext.SaveChangesAsync(cancellationToken);

      return Ok(_mapper.Map<IssueModel>(issue));
    }
  }
}
