using AutoMapper;
using Blazor.Application.DTOs;
using Blazor.Application.Features.Products.Queries;
using Blazor.Domain.Entities;
using Blazor.Infrastructure.ViewModel;

namespace Blazor.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();


            CreateMap<ProductPrice, ProductPriceDTO>().ReverseMap();
            CreateMap<OrderHeaderDTO, OrderHeader>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailDTO>().ReverseMap();
            CreateMap<OrderDTO, Order>().ReverseMap();
        }
    }
}
