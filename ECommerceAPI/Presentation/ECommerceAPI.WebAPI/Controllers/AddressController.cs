using ECommerceAPI.Application.Consts;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.Features.Addresses.Commands.Create;
using ECommerceAPI.Application.Features.Addresses.Commands.Delete;
using ECommerceAPI.Application.Features.Addresses.Commands.Update;
using ECommerceAPI.Application.Features.Addresses.Queries.GetAll;
using ECommerceAPI.Application.Features.Addresses.Queries.GetAllCustomerId;
using ECommerceAPI.Application.Features.Addresses.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IMediator _mediatR;

        public AddressController(IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        [Authorize(Roles = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Addresses, ActionType = Domain.Enum.ActionType.Reading, Definition = "Get All Address")]
        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediatR.Send(new GetAllAddressesRequest());
            return Ok(response);
        }

        [HttpGet]
        [Route("getAllByCostmerId/{customerId}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Addresses, ActionType = Domain.Enum.ActionType.Reading, Definition = "Get All Address By Customer Id")]
        public async Task<IActionResult> GetAllByCustomerId(int customerId)
        {
            var response = await _mediatR.Send(new GetAllAddressesByCustomerIdRequest(customerId));
            return Ok(response);
        }

        [HttpGet]
        [Route("getById/{id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Addresses, ActionType = Domain.Enum.ActionType.Reading, Definition = "Get Address By Id")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _mediatR.Send(new GetByIdAddressRequest(id));
            return Ok(response);
        }

  
        [HttpPost]
        [Route("create")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Addresses, ActionType = Domain.Enum.ActionType.Writing, Definition = "Create Address")]
        public async Task<IActionResult> Create([FromBody] CreateAddressCommand request)
        {
            var response = await _mediatR.Send(request);
            return Ok(response);
        }

        [HttpPut]
        [Route("update")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Addresses, ActionType = Domain.Enum.ActionType.Updating, Definition = "Update Address")]
        public async Task<IActionResult> Update([FromBody] UpdateAddressCommand request)
        {
            var response = await _mediatR.Send(request);
            return Ok(response);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Addresses, ActionType = Domain.Enum.ActionType.Deleting, Definition = "Delete Address")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediatR.Send(new DeleteAddressCommand(id));
            return Ok(response);
        }
    }
}
