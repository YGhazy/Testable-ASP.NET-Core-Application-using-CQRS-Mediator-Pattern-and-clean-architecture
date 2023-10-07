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

namespace Blazor.Application.Features.Products.Queries;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductDTO>>
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    private readonly IProductRepository productRepository;

    public GetProductsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IProductRepository productRepository)
        {
        this.mapper=mapper;
        this.unitOfWork=unitOfWork;
        this.productRepository=productRepository;
    }


    public async Task<List<ProductDTO>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
       var result= ( await productRepository.GetAll()).ToList();
        return result;
    }
}

