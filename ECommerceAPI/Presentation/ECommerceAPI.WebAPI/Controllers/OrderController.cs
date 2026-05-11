using ECommerceAPI.Application.Consts;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.Features.Orders.Commands.Create;
using ECommerceAPI.Application.Features.Orders.Commands.Update;
using ECommerceAPI.Application.Features.Orders.Queries.GetAll;
using ECommerceAPI.Application.Features.Orders.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediatR;

        public OrderController(IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        [HttpGet]
        [Route("getAllByCustomerId/{customerId}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = Domain.Enum.ActionType.Reading, Definition = "Get Orders By CustomerId")]
        public async Task<IActionResult> GetAllByCustomerId(int customerId)
        {
            var response = await _mediatR.Send(new GetAllOrdersByCustomerIdRequest(customerId));
            return Ok(response);
        }

        [HttpGet]
        [Route("getById/{id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = Domain.Enum.ActionType.Reading, Definition = "Get Order By Id")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _mediatR.Send(new GetByIdOrderRequest(id));
            return Ok(response);
        }

        [HttpPost]
        [Route("create")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = Domain.Enum.ActionType.Writing, Definition = "Create Order")]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand request)
        {
            var response = await _mediatR.Send(request);
            return Ok(response);
        }

        [HttpPut]
        [Route("updateOrderStatus")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = Domain.Enum.ActionType.Updating, Definition = "Update Order Status")]
        public async Task<IActionResult> UpdateOrderStatus([FromBody] UpdateOrderStatusCommand request)
        {
            var response = await _mediatR.Send(request);
            return Ok(response);
        }

    }
}
