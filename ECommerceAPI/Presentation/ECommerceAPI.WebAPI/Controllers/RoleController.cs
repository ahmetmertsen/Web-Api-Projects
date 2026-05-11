using ECommerceAPI.Application.Consts;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.Features.AppRole.Commands.Create;
using ECommerceAPI.Application.Features.AppRole.Commands.Delete;
using ECommerceAPI.Application.Features.AppRole.Commands.Update;
using ECommerceAPI.Application.Features.AppRole.Queries.GetAll;
using ECommerceAPI.Application.Features.AppRole.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediatR;

        public RoleController(IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Roles, ActionType = Domain.Enum.ActionType.Reading, Definition = "GetAll Roles")]
        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediatR.Send(new GetAllRolesRequest());
            return Ok(response);
        }

        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Roles, ActionType = Domain.Enum.ActionType.Reading, Definition = "Get Role By Id")]
        [HttpGet]
        [Route("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _mediatR.Send(new GetRoleByIdRequest { Id = id });
            return Ok(response);
        }

        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Roles, ActionType = Domain.Enum.ActionType.Writing, Definition = "Create Role")]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreateRoleCommand request)
        {
            var response = await _mediatR.Send(request);
            return Ok(response);
        }

        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Roles, ActionType = Domain.Enum.ActionType.Updating, Definition = "Update Role")]
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] UpdateRoleCommand request)
        {
            var response = await _mediatR.Send(request);
            return Ok(response);
        }

        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Roles, ActionType = Domain.Enum.ActionType.Deleting, Definition = "Delete Role")]
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediatR.Send(new DeleteRoleCommand { Id = id } );
            return Ok(response);
        }
    }
}
