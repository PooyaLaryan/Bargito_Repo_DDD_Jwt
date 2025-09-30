using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.Users.Query;

namespace OrderManagement.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginUserQuery loginUserCommand)
    {
        try
        {
            var result = await _mediator.Send(loginUserCommand);
            return Ok(result);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Invalid credentials");
        }
    }
}
