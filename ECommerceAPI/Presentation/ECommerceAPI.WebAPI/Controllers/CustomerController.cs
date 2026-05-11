using ECommerceAPI.Application.Consts;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.Features.Customers.Commands.Create;
using ECommerceAPI.Application.Features.Customers.Commands.Delete;
using ECommerceAPI.Application.Features.Customers.Commands.Update;
using ECommerceAPI.Application.Features.Customers.Queries.GetAll;
using ECommerceAPI.Application.Features.Customers.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediatR;
        public CustomerController(IMediator mediatR) 
        {
            _mediatR = mediatR;
        }

        [Authorize(Roles = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Customers, ActionType = Domain.Enum.ActionType.Reading, Definition = "Get All Customers")]
        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediatR.Send(new GetAllCustomersRequest());
            return Ok(response);
        }

        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Customers, ActionType = Domain.Enum.ActionType.Reading, Definition = "Get Customer By Id")]
        [HttpGet]
        [Route("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _mediatR.Send(new GetByIdCustomerRequest(id));
            return Ok(response);
        }

        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Customers, ActionType = Domain.Enum.ActionType.Writing, Definition = "Create Customer")]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreateCustomerCommand request)
        {
            var response = await _mediatR.Send(request);
            return Ok(response);
        }

        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Customers, ActionType = Domain.Enum.ActionType.Updating, Definition = "Update Customer")]
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] UpdateCustomerCommand request)
        {
            var response = await _mediatR.Send(request);
            return Ok(response);
        }

        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Customers, ActionType = Domain.Enum.ActionType.Deleting, Definition = "Delete Customer")]
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediatR.Send(new DeleteCustomerCommand(id));
            return Ok(response);
        }
    }
}
