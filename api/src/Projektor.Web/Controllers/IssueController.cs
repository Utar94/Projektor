﻿using MediatR;
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
  [Route("issues")]
  public class IssueController : ControllerBase
  {
    private readonly IMediator _mediator;

    public IssueController(IMediator mediator)
    {
      _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<IssueModel>> CreateAsync(
      [FromBody] CreateIssuePayload payload,
      CancellationToken cancellationToken
    )
    {
      IssueModel model = await _mediator.Send(new CreateIssueCommand(payload), cancellationToken);
      var uri = new Uri($"/issues/{model.Id}", UriKind.Relative);

      return Created(uri, model);
    }

    [HttpGet]
    public async Task<ActionResult<ListModel<IssueModel>>> GetAsync(
      bool? closed,
      bool? deleted,
      Priority? priority,
      Guid? projectId,
      Resolution? resolution,
      string? search,
      Guid? typeId,
      IssueSort? sort,
      bool desc,
      int? index,
      int? count,
      CancellationToken cancellationToken
    )
    {
      return Ok(await _mediator.Send(new GetIssuesQuery
      {
        Closed = closed,
        Deleted = deleted,
        Priority = priority,
        ProjectId = projectId,
        Resolution = resolution,
        Search = search,
        TypeId = typeId,
        Sort = sort,
        Desc = desc,
        Index = index,
        Count = count
      }, cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IssueModel>> GetAsync(string id, CancellationToken cancellationToken)
    {
      return Ok(await _mediator.Send(new GetIssueQuery(id), cancellationToken));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<IssueModel>> UpdateAsync(
      Guid id,
      [FromBody] UpdateIssuePayload payload,
      CancellationToken cancellationToken
    )
    {
      return Ok(await _mediator.Send(new UpdateIssueCommand(id, payload), cancellationToken));
    }

    [HttpPatch("{id}/close")]
    public async Task<ActionResult<IssueModel>> CloseIssueAsync(
      Guid id,
      [FromBody] CloseIssuePayload payload,
      CancellationToken cancellationToken
    )
    {
      return Ok(await _mediator.Send(new CloseIssueCommand(id, payload), cancellationToken));
    }

    [HttpPatch("{id}/reopen")]
    public async Task<ActionResult<IssueModel>> ReopenIssueAsync(Guid id, CancellationToken cancellationToken)
    {
      return Ok(await _mediator.Send(new ReopenIssueCommand(id), cancellationToken));
    }
  }
}
