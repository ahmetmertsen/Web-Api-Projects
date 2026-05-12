using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.Features.AuthorizationEndpoint.Commands.AssignRoleEndpoint;
using ECommerceAPI.Application.Features.AuthorizationEndpoint.Queries.GetRolesToEndpoint;
using ECommerceAPI.Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationEndpointsController : ControllerBase
    {
        private readonly IMediator _mediatR;

        public AuthorizationEndpointsController(IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        [Authorize(Roles = "Admin")]
        [AuthorizeDefinition(Menu = "Authorization Endpoints", ActionType = ActionType.Reading, Definition = "Get Roles To Endpoint")]
        [HttpPost]
        [Route("getRolesToEndpoint")]
        public async Task<IActionResult> GetRolesToEndpoint([FromBody] GetRolesToEndpointQueryRequest request)
        {
            var response = await _mediatR.Send(request);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [AuthorizeDefinition(Menu = "Authorization Endpoints", ActionType = ActionType.Writing, Definition = "Assign Role Endpoint")]
        [HttpPost]
        public async Task<IActionResult> AssignRoleEndpoint(AssignRoleEndpointCommand request)
        {
            request.Type = typeof(Program);

            var response = await _mediatR.Send(request);
            return Ok(response);
        }
    }
}
