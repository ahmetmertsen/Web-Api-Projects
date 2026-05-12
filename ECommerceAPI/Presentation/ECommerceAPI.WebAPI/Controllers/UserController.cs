using ECommerceAPI.Application.Consts;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.Features.AppUser.Commands.AssignRoleToUser;
using ECommerceAPI.Application.Features.AppUser.Commands.Create;
using ECommerceAPI.Application.Features.AppUser.Queries.GetAll;
using ECommerceAPI.Application.Features.AppUser.Queries.GetRolesToUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediatR;

        public UserController(IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand request)
        {
            var response = await _mediatR.Send(request);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Users, ActionType = Domain.Enum.ActionType.Reading, Definition = "GetAll Users")]
        [HttpGet]
        [Route("getAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _mediatR.Send(new GetAllUsersQueryRequest());
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Users, ActionType = Domain.Enum.ActionType.Reading, Definition = "Get Roles To User")]
        [HttpGet]
        [Route("getRolesToUser/{userId}")]
        public async Task<IActionResult> GetRolesToUser(int userId)
        {
            var response = await _mediatR.Send(new GetRolesToUserQueryRequest() { UserId = userId });
            return Ok(response);
        }


        [Authorize(Roles = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Users, ActionType = Domain.Enum.ActionType.Writing, Definition = "Assign Role To User")]
        [HttpPost]
        [Route("assignRoleToUser")]
        public async Task<IActionResult> AssignRoleToUser([FromBody] AssignRoleToUserCommand request)
        {
            var response = await _mediatR.Send(request);
            return Ok(response);
        }

    }
}
