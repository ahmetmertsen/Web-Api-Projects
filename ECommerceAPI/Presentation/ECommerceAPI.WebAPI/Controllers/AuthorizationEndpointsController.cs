using ECommerceAPI.Application.Features.AuthorizationEndpoint.Commands.AssignRoleEndpoint;
using ECommerceAPI.Application.Features.AuthorizationEndpoint.Queries.GetRolesToEndpoint;
using MediatR;
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

        [HttpPost]
        [Route("getRolesToEndpoint")]
        public async Task<IActionResult> GetRolesToEndpoint([FromBody] GetRolesToEndpointQueryRequest request)
        {
            var response = await _mediatR.Send(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleEndpoint(AssignRoleEndpointCommand request)
        {
            request.Type = typeof(Program);

            var response = await _mediatR.Send(request);
            return Ok(response);
        }
    }
}
