using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projektor.Core.Models;
using Projektor.Core.Worklogs;
using Projektor.Core.Worklogs.Commands;
using Projektor.Core.Worklogs.Models;
using Projektor.Core.Worklogs.Payloads;
using Projektor.Core.Worklogs.Queries;

namespace Projektor.Web.Controllers
{
  [ApiController]
  [Authorize]
  [Route("worklogs")]
  public class WorklogController : ControllerBase
  {
    private readonly IMediator _mediator;

    public WorklogController(IMediator mediator)
    {
      _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<WorklogModel>> CreateAsync(
      [FromBody] CreateWorklogPayload payload,
      CancellationToken cancellationToken
    )
    {
      WorklogModel model = await _mediator.Send(new CreateWorklogCommand(payload), cancellationToken);
      var uri = new Uri($"/worklogs/{model.Id}", UriKind.Relative);

      return Created(uri, model);
    }

    [HttpGet]
    public async Task<ActionResult<ListModel<WorklogModel>>> GetAsync(
      bool? deleted,
      Guid? issueId,
      WorklogSort? sort,
      bool desc,
      int? index,
      int? count,
      CancellationToken cancellationToken
    )
    {
      return Ok(await _mediator.Send(new GetWorklogsQuery
      {
        Deleted = deleted,
        IssueId = issueId,
        Sort = sort,
        Desc = desc,
        Index = index,
        Count = count
      }, cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WorklogModel>> GetAsync(Guid id, CancellationToken cancellationToken)
    {
      return Ok(await _mediator.Send(new GetWorklogQuery(id), cancellationToken));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<WorklogModel>> UpdateAsync(
      Guid id,
      [FromBody] UpdateWorklogPayload payload,
      CancellationToken cancellationToken
    )
    {
      return Ok(await _mediator.Send(new UpdateWorklogCommand(id, payload), cancellationToken));
    }
  }
}
