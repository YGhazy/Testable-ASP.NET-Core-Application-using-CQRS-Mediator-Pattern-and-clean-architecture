using AutoMapper;
using Blazor.Application.DTOs;
using Blazor.Application.Features.Products.Commands.CreateProductCommand;
using Blazor.Application.Features.Products.Queries;
using Blazor.Domain.Entities;
using Features.Categories.Commands;
using Features.Categories.Queries.GetCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Application.Mapper;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<CreateCategoryCommand, CategoryDTO>();
    }
}
