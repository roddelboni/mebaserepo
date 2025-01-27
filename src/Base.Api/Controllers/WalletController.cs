using Base.Application.Wallet.Commands.AddBalance;
using Base.Application.Wallet.Commands.TransferBalance;
using Base.Application.Wallet.Queries.GetWalletByCustomerId;
using Base.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Base.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class WalletController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WalletController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Result<GetWalletByCustomerIdResponse>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get([Required] int id)
        {
            var response = await _mediator.Send(new GetWalletByCustomerIdRequest(Id: id));
            return response.ToActionResult(this);
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(Result<GetWalletByCustomerIdResponse>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Put(AddBalanceRequest request)
        {
            var response = await _mediator.Send(request);
            return response.ToActionResult(this);
        }

        [HttpPut("transfer")]
        [ProducesResponseType(200, Type = typeof(Result<TransferBalanceResponse>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Transfer(TransferBalanceRequest request)
        {
            var response = await _mediator.Send(request);
            return response.ToActionResult(this);
        }
    }
}
