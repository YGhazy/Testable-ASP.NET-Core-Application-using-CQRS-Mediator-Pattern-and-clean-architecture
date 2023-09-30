using AutoMapper;
using Blazor.Application.Common;
using Blazor.Application.DTOs;
using Blazor.Application.IRepository;
using Blazor.Application.Services.IServices;
using Blazor.Domain.Common;
using Microsoft.AspNetCore.Http;
using Stripe.Checkout;

namespace Blazor.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        public ProductService(IMapper mapper, IUnitOfWork unitOfWork, IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<ProductDTO>> Get(int? productId)
        {
            ApiResponse<ProductDTO> result = new ApiResponse<ProductDTO>();
            if (productId==null || productId==0)
            {
                result.Succeeded = false;
                result.Errors.Add("Invalid Id");
                //StatusCode=StatusCodes.Status400BadRequest
                return result;
            }

            var product = await _productRepository.Get(productId.Value);
            if (product==null)
            {
                result.Succeeded = false;
                result.Errors.Add("Invalid Id");
                //StatusCode=StatusCodes.Status400BadRequest
                return result;
            }
            result.Succeeded = true;
            result.Data = product;
            return result;
        }

        public async Task<ApiResponse<IEnumerable<ProductDTO>>> GetAll()
        {
          ApiResponse<IEnumerable<ProductDTO>> result = new ApiResponse<IEnumerable<ProductDTO>>();
                result.Succeeded = true;
                result.Data =  await _productRepository.GetAll();
                return result;
        }
}

}
