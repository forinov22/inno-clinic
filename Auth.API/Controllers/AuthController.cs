using Auth.Application.Accounts.Commands.Refresh;
using Auth.Application.Accounts.Commands.SignIn;
using Auth.Application.Accounts.Commands.SignUpPatient;
using Auth.Application.Accounts.Commands.SignUpStuff;
using Auth.Application.Accounts.Commands.VerifyEmail;
using Auth.Application.Accounts.Common;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Auth.API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("patient/sign-up")]
    public async Task<ActionResult<AuthResult>> SignUpPatient([FromBody] SignUpPatientAccountCommand signUpPatientAccountCommand)
    {
        var authResult = await mediator.Send(signUpPatientAccountCommand);
        Response.Cookies.Append("refreshToken", authResult.RefreshToken, new CookieOptions { HttpOnly = true });
        return authResult;
    }

    [HttpPost("stuff/sign-up")]
    public async Task<ActionResult<AccountResult>> SignUpStuff([FromBody] SignUpStuffAccountCommand signUpStuffAccountCommand)
    {
        var stuffAccount = await mediator.Send(signUpStuffAccountCommand);
        return stuffAccount;
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<AuthResult>> Refresh()
    {
        if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
        {
            return Unauthorized();
        }

        var authResult = await mediator.Send(new RefreshAccountCommand(refreshToken));
        Response.Cookies.Append("refreshToken", authResult.RefreshToken, new CookieOptions { HttpOnly = true });
        return authResult;
    }

    [HttpPost("sign-in")]
    public async Task<ActionResult<AuthResult>> SignIn([FromBody] SignInAccountCommand signInAccountCommand)
    {
        var authResult = await mediator.Send(signInAccountCommand);
        Response.Cookies.Append("refreshToken", authResult.RefreshToken, new CookieOptions { HttpOnly = true });
        return authResult;
    }

    [HttpPost("sign-out")]
    public IActionResult SignOut()
    {
        Response.Cookies.Delete("refreshToken");
        return NoContent();
    }

    [HttpGet("verify-email")]
    public async Task<IActionResult> VerifyEmail([FromQuery] string activationLink)
    {
        await mediator.Send(new VerifyEmailCommand(activationLink));
        return NoContent();
    }
}