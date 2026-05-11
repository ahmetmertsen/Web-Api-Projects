using ECommerceAPI.Application.Consts;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.Features.Carts.Commands.Create;
using ECommerceAPI.Application.Features.CartsItems.Commands.Create;
using ECommerceAPI.Application.Features.CartsItems.Commands.Delete;
using ECommerceAPI.Application.Features.CartsItems.Commands.Update;
using ECommerceAPI.Application.Features.CartsItems.Queries.GetAll;
using ECommerceAPI.Application.Features.CartsItems.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace ECommerceAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly IMediator _mediatR;

        public CartItemController(IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        [HttpGet]
        [Route("getAllByCartId/{cartId}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.CartItems, ActionType = Domain.Enum.ActionType.Reading, Definition = "Get Cart Items By Cart Id")]
        public async Task<IActionResult> GetAllByCartId(int cartId)
        {
            var response = await _mediatR.Send(new GetAllCartItemsByCartIdRequest(cartId));
            return Ok(response);
        }

        [HttpGet]
        [Route("getById/{id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.CartItems, ActionType = Domain.Enum.ActionType.Reading, Definition = "Get Cart Items By Id")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _mediatR.Send(new GetByIdCartItemRequest(id));
            return Ok(response);
        }

        [HttpPost]
        [Route("create")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.CartItems, ActionType = Domain.Enum.ActionType.Writing, Definition = "Create Cart Item")]
        public async Task<IActionResult> Create([FromBody] CreateCartItemCommand request)
        {
            var response = await _mediatR.Send(request);
            return Ok(response);
        }

        [HttpPut]
        [Route("update")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.CartItems, ActionType = Domain.Enum.ActionType.Updating, Definition = "Update Cart Item")]
        public async Task<IActionResult> Update([FromBody] UpdateCartItemCommand request)
        {
            var response = await _mediatR.Send(request);
            return Ok(response);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.CartItems, ActionType = Domain.Enum.ActionType.Deleting, Definition = "Delete Cart Item")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediatR.Send(new DeleteCartItemCommand(id));
            return Ok(response);
        }


    }
}
