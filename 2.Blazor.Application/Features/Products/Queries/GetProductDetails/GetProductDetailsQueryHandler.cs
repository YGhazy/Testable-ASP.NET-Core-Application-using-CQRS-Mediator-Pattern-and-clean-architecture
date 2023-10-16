using AutoMapper;
using Blazor.Application.Common;
using Blazor.Application.IRepository;
using Blazor.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Application.Features.Products.Queries.GetProductDetails
{
    public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, ProductDTO>
    {
        private readonly IMapper mapper;
        private readonly IProductRepository productRepository;

        public GetProductDetailsQueryHandler(IMapper mapper, IProductRepository productRepository)
        {
            this.mapper=mapper;
            this.productRepository=productRepository;
        }
        public async Task<ProductDTO> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
        {

            var product = await productRepository.Get(request.ID);

            return product; 
        }
    }
}
