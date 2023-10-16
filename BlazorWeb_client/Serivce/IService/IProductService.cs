using Blazor.Application.DTOs;
using Blazor.Application.Features.Products.Queries;

namespace BlazorWeb_Client.Serivce.IService
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDTO>> GetAll();
        public Task<ProductDTO> Get(int productId);
    }
}
