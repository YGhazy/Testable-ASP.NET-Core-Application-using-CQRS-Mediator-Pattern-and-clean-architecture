using Blazor.Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Blazor.Domain.Entities;
using Blazor.Application.Features.Products.Queries;
using AutoMapper;
using Blazor.Application.Mapper;

namespace Blazor.Application.UnitTests.Mocks;

public class MockProductRepository
{

    public MockProductRepository( )

    {
    }
    public static Mock<IProductRepository> GetMockProductsRepository()
    {
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<ProductProfile>();
        });
        IMapper _mapper = mapperConfig.CreateMapper();
     
        var Products = new List<ProductDTO>
            {
                new ProductDTO
                {
                    Id = 1,
                    Name = "Test Vacation"
                },
                new ProductDTO
                {
                    Id = 2,
                    Name = "Test Sick"
                },
                new ProductDTO
                {
                    Id = 3,
                    Name = "Test Maternity"
                }
            };

        var mockRepo = new Mock<IProductRepository>();

        mockRepo.Setup(r => r.GetAll()).ReturnsAsync(Products);

        mockRepo.Setup(r => r.Create(It.IsAny<ProductDTO>()))
            .Returns((ProductDTO prod) =>
            {
                Products.Add(prod);
                return Task.FromResult( prod);
            });

        mockRepo.Setup(r => r.IsProductUnique(It.IsAny<string>()))
            .ReturnsAsync((string name) =>
            {
                return !Products.Any(q => q.Name == name);
            });

        return mockRepo;
    }
}
