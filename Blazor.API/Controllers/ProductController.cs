using Blazor.API.Common;
using Blazor.Application.DTOs;
using Blazor.Application.IRepository;
using Blazor.Application.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseResultHandlerController<IProductService>
    {
        private readonly IProductService _ProductService;
        public ProductController(IProductService ProductService) : base(ProductService)
        {
            _ProductService = ProductService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return await GetResponseHandler(async () => await service.GetAll());
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> Get(int? productId)
        {
            return await GetResponseHandler(async () => await service.Get(productId));
        }
    }
}
