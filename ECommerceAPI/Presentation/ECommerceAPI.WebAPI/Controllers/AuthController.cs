using ECommerceAPI.Application.Features.AppUser.Commands.Create;
using ECommerceAPI.Application.Features.AppUser.Commands.Login;
using ECommerceAPI.Application.Features.AppUser.Commands.RefreshTokenLogin;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediatR;

        public AuthController(IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand request)
        {
            var response = await _mediatR.Send(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("refreshToken")]
        public async Task<IActionResult> RefreshTokenLogin([FromBody] RefreshTokenLoginCommand request)
        {
            var response = await _mediatR.Send(request);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("whoami")]
        public IActionResult WhoAmI()
        {
            var roles = User.Claims
                .Where(x => x.Type == ClaimTypes.Role)
                .Select(x => x.Value)
                .ToList();

            return Ok(new
            {
                name = User.Identity?.Name,
                roles,
                claims = User.Claims.Select(c => new { c.Type, c.Value })
            });
        }
    }
}
