using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projektor.Core.Models;
using Projektor.Core.Projects;
using Projektor.Core.Projects.Commands;
using Projektor.Core.Projects.Models;
using Projektor.Core.Projects.Payloads;
using Projektor.Core.Projects.Queries;

namespace Projektor.Web.Controllers
{
  [ApiController]
  [Authorize]
  [Route("projects")]
  public class ProjectController : ControllerBase
  {
    private readonly IMediator _mediator;

    public ProjectController(IMediator mediator)
    {
      _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<ProjectModel>> CreateAsync(
      [FromBody] CreateProjectPayload payload,
      CancellationToken cancellationToken
    )
    {
      ProjectModel model = await _mediator.Send(new CreateProjectCommand(payload), cancellationToken);
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
      return Ok(await _mediator.Send(new GetProjectsQuery
      {
        Deleted = deleted,
        Search = search,
        Sort = sort,
        Desc = desc,
        Index = index,
        Count = count
      }, cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectModel>> GetAsync(string id, CancellationToken cancellationToken)
    {
      return Ok(await _mediator.Send(new GetProjectQuery(id), cancellationToken));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectModel>> UpdateAsync(
      Guid id,
      [FromBody] UpdateProjectPayload payload,
      CancellationToken cancellationToken
    )
    {
      return Ok(await _mediator.Send(new UpdateProjectCommand(id, payload), cancellationToken));
    }
  }
}
