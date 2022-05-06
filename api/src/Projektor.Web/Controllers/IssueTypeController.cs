using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projektor.Core.Issues;
using Projektor.Core.Issues.Commands;
using Projektor.Core.Issues.Models;
using Projektor.Core.Issues.Payloads;
using Projektor.Core.Issues.Queries;
using Projektor.Core.Models;

namespace Projektor.Web.Controllers
{
  [ApiController]
  [Authorize]
  [Route("issues/types")]
  public class IssueTypeController : ControllerBase
  {
    private readonly IMediator _mediator;

    public IssueTypeController(IMediator mediator)
    {
      _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<IssueTypeModel>> CreateAsync(
      [FromBody] CreateIssueTypePayload payload,
      CancellationToken cancellationToken
    )
    {
      IssueTypeModel model = await _mediator.Send(new CreateIssueTypeCommand(payload), cancellationToken);
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
      return Ok(await _mediator.Send(new GetIssueTypesQuery
      {
        Deleted = deleted,
        ProjectId = projectId,
        Search = search,
        Sort = sort,
        Desc = desc,
        Index = index,
        Count = count
      }, cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IssueTypeModel>> GetAsync(Guid id, CancellationToken cancellationToken)
    {
      return Ok(await _mediator.Send(new GetIssueTypeQuery(id), cancellationToken));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<IssueTypeModel>> UpdateAsync(
      Guid id,
      [FromBody] UpdateIssueTypePayload payload,
      CancellationToken cancellationToken
    )
    {
      return Ok(await _mediator.Send(new UpdateIssueTypeCommand(id, payload), cancellationToken));
    }
  }
}
