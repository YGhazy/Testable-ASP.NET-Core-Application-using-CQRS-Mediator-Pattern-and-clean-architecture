using AutoMapper;
using Blazor.Application.Features.Products.Commands.CreateProductCommand;
using Blazor.Application.Features.Products.Queries;
using Blazor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Application.Mapper;

internal class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDTO>().ReverseMap();
        //CreateMap<Product, ProductDetailsDto>();
        CreateMap<CreateProductCommand, ProductDTO>();
        //CreateMap<UpdateProductCommand, Product>();
    }
}
