using Logitar.AspNetCore.Identity;
using Logitar.Identity.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projektor.Infrastructure;
using Projektor.Web.Models.Identity;

namespace Projektor.Web.Controllers
{
  [ApiController]
  [Route("identity")]
  public class IdentityController : ControllerBase
  {
    private readonly ProjektorDbContext _dbContext;
    private readonly IIdentityService _identityService;

    public IdentityController(ProjektorDbContext dbContext, IIdentityService identityService)
    {
      _dbContext = dbContext;
      _identityService = identityService;
    }

    [HttpPost("confirm")]
    public async Task<ActionResult> ConfirmAsync(
      [FromBody] ConfirmPayload payload,
      CancellationToken cancellationToken
    )
    {
      await _identityService.ConfirmAsync(payload, cancellationToken);

      return NoContent();
    }

    [Authorize]
    [HttpPost("password/change")]
    public async Task<ActionResult> ChangePasswordAsync(
      [FromBody] ChangePasswordPayload payload,
      CancellationToken cancellationToken
    )
    {
      await _identityService.ChangePasswordAsync(payload, cancellationToken);

      return NoContent();
    }

    [HttpPost("password/recover")]
    public async Task<ActionResult> RecoverPasswordAsync(
      [FromBody] RecoverPasswordPayload payload,
      CancellationToken cancellationToken
    )
    {
      RecoverPasswordResult result = await _identityService.RecoverPasswordAsync(payload, cancellationToken);

      // TODO(fpion): send password recovery email

      return NoContent();
    }

    [HttpPost("password/reset")]
    public async Task<ActionResult> ResetPasswordAsync(
      [FromBody] ResetPasswordPayload payload,
      CancellationToken cancellationToken
    )
    {
      await _identityService.ResetPasswordAsync(payload, cancellationToken);

      return NoContent();
    }

    [Authorize]
    [HttpGet("profile")]
    public async Task<ActionResult<ProfileModel>> GetProfileAsync(CancellationToken cancellationToken)
    {
      User user = await _identityService.GetUserAsync(cancellationToken);

      return Ok(new ProfileModel(user));
    }

    [Authorize]
    [HttpPut("profile")]
    public async Task<ActionResult<ProfileModel>> SaveProfileAsync(
      [FromBody] SaveUserPayload payload,
      CancellationToken cancellationToken
    )
    {
      User user = await _identityService.SaveUserAsync(payload, cancellationToken);

      return Ok(new ProfileModel(user));
    }

    [HttpPost("renew")]
    public async Task<ActionResult<TokenModel>> RenewAsync(
      [FromBody] RenewPayload payload,
      CancellationToken cancellationToken
    )
    {
      return Ok(await _identityService.RenewAsync(payload, cancellationToken));
    }

    [HttpPost("sign/in")]
    public async Task<ActionResult<TokenModel>> SignInAsync(
      [FromBody] SignInPayload payload,
      CancellationToken cancellationToken
    )
    {
      return Ok(await _identityService.SignInAsync(payload, cancellationToken));
    }

    [Authorize]
    [HttpPost("sign/out")]
    public async Task<ActionResult> SignOutAsync(
      [FromBody] SignOutPayload payload,
      CancellationToken cancellationToken
    )
    {
      await _identityService.SignOutAsync(payload, cancellationToken);

      return NoContent();
    }

    [HttpPost("sign/up")]
    public async Task<ActionResult> SignUpAsync(
      [FromBody] SignUpPayload payload,
      CancellationToken cancellationToken
    )
    {
      bool superuser = !(await _dbContext.Users.AnyAsync(cancellationToken));

      SignUpResult result = await _identityService.SignUpAsync(payload, cancellationToken: cancellationToken);

      if (result.Token != null)
      {
        if (superuser)
        {
          return Ok(new ConfirmPayload
          {
            Id = result.User.Id,
            Token = result.Token
          });
        }

        // TODO(fpion): send account creation email
      }

      return NoContent();
    }
  }
}
