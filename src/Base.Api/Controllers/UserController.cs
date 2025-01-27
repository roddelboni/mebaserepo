using Base.Application.Users.Commands.CreateUser;
using Base.Application.Users.Commands.Logins;
using Base.Application.Wallet.Queries.GetWalletByCustomerId;
using Base.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Base.Api.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(200, Type = typeof(Result<CreateUserResponse>))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Create(CreateUserRequest request)
    {
        var response = await _mediator.Send(request);
        return NoContent();
    }


    [HttpPut("login")]
    [AllowAnonymous]
    [ProducesResponseType(200, Type = typeof(Result<LoginResponse>))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Login(string email, string password)
    {
        var request = new LoginRequest(email, password);
        var response = await _mediator.Send(request);
        return response.ToActionResult(this);
    }
}