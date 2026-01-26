using ECommerceAPI.Application.Features.Products.Commands.Create;
using ECommerceAPI.Application.Features.Products.Commands.Delete;
using ECommerceAPI.Application.Features.Products.Commands.Update;
using ECommerceAPI.Application.Features.Products.Queries.GetAll;
using ECommerceAPI.Application.Features.Products.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediatR;

        public ProductController(IMediator mediator)
        {
            _mediatR = mediator;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediatR.Send(new GetAllProductsRequest());
            return Ok(response);
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _mediatR.Send(new GetByIdProductRequest(id));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand request)
        {
            var response = await _mediatR.Send(request);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommand request)
        {
            var response = await _mediatR.Send(request);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id) 
        {
            var response = await _mediatR.Send(new DeleteProductCommand(id));
            return Ok(response);
        }
    }
}
