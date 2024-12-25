﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupleNet.Application.Responses.Common;
using SupleNet.Application.UseCases.ItemCart.Command.AddItemCart;
using SupleNet.Application.UseCases.ItemCart.Queries.GetCartUser;

namespace SupleNet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("current")]
        [ProducesResponseType<Result<GetCartUserQueryResponse>>(StatusCodes.Status200OK)]
        public async Task<ActionResult<Result<GetCartUserQueryResponse>>> GetCurrentUserCart()
        {
            var result = await _mediator.Send(new GetCartUserQuery());
            return StatusCode((int)result.HttpStatusCode, result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("addItem")]
        [ProducesResponseType<Result<Unit>>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Result<Unit>>> AddItemCart(AddItemCartCommand command)
        {
            var result = await _mediator.Send(command);
            if(result.IsSuccess)
                return StatusCode((int)result.HttpStatusCode, result);
            return StatusCode((int)result.HttpStatusCode, result.Message);
        }

    }
}