using Blazor.Application.Common;
using Blazor.Application.DTOs;

namespace Blazor.Application.Services.IServices
{
    public interface IProductService
    {
        public Task<ApiResponse<IEnumerable<ProductDTO>>> GetAll();
        public Task<ApiResponse<ProductDTO>> Get(int? productId);
    }
}
