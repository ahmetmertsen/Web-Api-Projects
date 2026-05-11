using ECommerceAPI.Application.Consts;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.Features.Payments.Commands.Create;
using ECommerceAPI.Application.Features.Payments.Commands.UpdatePaymentStatus;
using ECommerceAPI.Application.Features.Payments.Queries.GetAll;
using ECommerceAPI.Application.Features.Payments.Queries.GetById.GetPaymentById;
using ECommerceAPI.Application.Features.Payments.Queries.GetById.GetPaymentByOrderId;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediatR;

        public PaymentController(IMediator mediatR) 
        {
            _mediatR = mediatR;
        }

        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Payments, ActionType = Domain.Enum.ActionType.Reading, Definition = "Get All Payments")]
        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediatR.Send(new GetAllPaymentsRequest());
            return Ok(response);
        }

        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Payments, ActionType = Domain.Enum.ActionType.Reading, Definition = "Get Payment By Id")]
        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _mediatR.Send(new GetPaymentByIdRequest(id));
            return Ok(response);
        }

        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Payments, ActionType = Domain.Enum.ActionType.Reading, Definition = "Get Payment By OrderId")]
        [HttpGet]
        [Route("getByOrderId/{orderId}")]
        public async Task<IActionResult> GetByOrderId(int orderId)
        {
            var response = await _mediatR.Send(new GetPaymentByOrderIdRequest(orderId));
            return Ok(response);
        }

        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Payments, ActionType = Domain.Enum.ActionType.Writing, Definition = "Create Payment")]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreatePaymentCommand request)
        {
            var response = await _mediatR.Send(request);
            return Ok(response);
        }

        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Payments, ActionType = Domain.Enum.ActionType.Updating, Definition = "Update Payment Status")]
        [HttpPut]
        [Route("updateStatus")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdatePaymentStatusCommand request)
        {
            var response = await _mediatR.Send(request);
            return Ok(response);
        }




    }
}
