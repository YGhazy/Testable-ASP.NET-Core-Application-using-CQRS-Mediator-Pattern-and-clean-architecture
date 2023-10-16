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
        var Products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Test Vacation"
                },
                new Product
                {
                    Id = 2,
                    Name = "Test Sick"
                },
                new Product
                {
                    Id = 3,
                    Name = "Test Maternity"
                }
            };

        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<ProductProfile>();
        });
        IMapper _mapper = mapperConfig.CreateMapper();
        var mockRepo = new Mock<IProductRepository>();

        mockRepo.Setup(r => r.GetAll()).ReturnsAsync(_mapper.Map<IEnumerable<ProductDTO>>(Products));

        mockRepo.Setup(r => r.Create(It.IsAny<ProductDTO>()))
            .Returns((ProductDTO prod) =>
            {
                Products.Add(_mapper.Map<Product>(prod));
                return Task.FromResult(prod);
            });

        mockRepo.Setup(r => r.IsProductUnique(It.IsAny<string>()))
            .ReturnsAsync((string name) =>
            {
                return !Products.Any(q => q.Name == name);
            });

        return mockRepo;
    }
}
