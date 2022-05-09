using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projektor.Core.Comments;
using Projektor.Core.Comments.Commands;
using Projektor.Core.Comments.Models;
using Projektor.Core.Comments.Payloads;
using Projektor.Core.Comments.Queries;
using Projektor.Core.Models;

namespace Projektor.Web.Controllers
{
  [ApiController]
  [Authorize]
  [Route("comments")]
  public class CommentController : ControllerBase
  {
    private readonly IMediator _mediator;

    public CommentController(IMediator mediator)
    {
      _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<CommentModel>> CreateAsync(
      [FromBody] CreateCommentPayload payload,
      CancellationToken cancellationToken
    )
    {
      CommentModel model = await _mediator.Send(new CreateCommentCommand(payload), cancellationToken);
      var uri = new Uri($"/comments/{model.Id}", UriKind.Relative);

      return Created(uri, model);
    }

    [HttpGet]
    public async Task<ActionResult<ListModel<CommentModel>>> GetAsync(
      bool? deleted,
      Guid? issueId,
      CommentSort? sort,
      bool desc,
      int? index,
      int? count,
      CancellationToken cancellationToken
    )
    {
      return Ok(await _mediator.Send(new GetCommentsQuery
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
    public async Task<ActionResult<CommentModel>> GetAsync(Guid id, CancellationToken cancellationToken)
    {
      return Ok(await _mediator.Send(new GetCommentQuery(id), cancellationToken));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CommentModel>> UpdateAsync(
      Guid id,
      [FromBody] UpdateCommentPayload payload,
      CancellationToken cancellationToken
    )
    {
      return Ok(await _mediator.Send(new UpdateCommentCommand(id, payload), cancellationToken));
    }
  }
}
