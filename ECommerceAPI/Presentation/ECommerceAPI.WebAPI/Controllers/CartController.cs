using ECommerceAPI.Application.Consts;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.Features.Carts.Commands.Create;
using ECommerceAPI.Application.Features.Carts.Queries.GetCartByCustomerId;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediatR;

        public CartController(IMediator mediator)
        {
            _mediatR = mediator;
        }

        [HttpGet]
        [Route("getByCustomerId/{customerId}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Carts, ActionType = Domain.Enum.ActionType.Reading, Definition = "Get Cart By CustomerId")]
        public async Task<IActionResult> GetCartByCustomerId(int customerId)
        {
            var response = await _mediatR.Send(new GetCartByCustomerIdRequest(customerId));
            return Ok(response);
        }

        [HttpPost]
        [Route("create")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Carts, ActionType = Domain.Enum.ActionType.Writing, Definition = "Create Cart")]
        public async Task<IActionResult> Create([FromBody] CreateCartCommand request)
        {
            var response = await _mediatR.Send(request);
            return Ok(response);
        }
    }
}
