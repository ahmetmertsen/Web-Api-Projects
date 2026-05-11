using ECommerceAPI.Application.Abstractions.Services.Configurations;
using ECommerceAPI.Application.Consts;
using ECommerceAPI.Application.CustomAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ApplicationServiceController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationServiceController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [AuthorizeDefinition(Menu = "Application Services", ActionType = Domain.Enum.ActionType.Reading, Definition = "Get Authorize Definition Endpoints")]
        [HttpGet]
        public IActionResult GetAuthorizeDefinitionEndpoints()
        {
            var datas = _applicationService.GetAuthorizeDefinitionEndpoints(typeof(Program));
            return Ok(datas);
        }
    }
}
