using AutoMapper;
using Blazor.Application.Common;
using Blazor.Application.Contracts.Logging;
using Blazor.Application.IRepository;
using Blazor.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Application.Features.Products.Queries;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductDTO>>
{
    private readonly IMapper mapper;
    private readonly IProductRepository productRepository;
    private readonly IAppLogger<GetProductsQueryHandler> logger;

    public GetProductsQueryHandler(IMapper mapper, IProductRepository productRepository,
        IAppLogger<GetProductsQueryHandler> logger)
        {
        this.mapper=mapper;
        this.productRepository=productRepository;
        this.logger=logger;
    }


    public async Task<List<ProductDTO>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
       var result= ( await productRepository.GetAll()).ToList();
        logger.LogInformation("Products were retrieved successfully");

        return result;
    }
}

