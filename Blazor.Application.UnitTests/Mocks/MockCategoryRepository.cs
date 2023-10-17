using Blazor.Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Blazor.Domain.Entities;
using AutoMapper;
using Blazor.Application.Mapper;
using Features.Categories.Queries.GetCategories;
using System.Xml.Linq;

namespace Blazor.Application.UnitTests.Mocks;

public class MockCategoryRepository
{

    public MockCategoryRepository( )

    {
    }
    public static Mock<ICategoryRepository> GetMockCategoriesRepository()
    {
        var Categories = new List<CategoryDTO>
            {
                new CategoryDTO
                {
                    Id = 1,
                    Name = "Category 1",
                    Description="Description Category 1"
                },
                new CategoryDTO
                {
                    Id = 2,
                    Name = "Category 2",
                    Description="Description Category 2"


                },
                new CategoryDTO
                {
                    Id = 3,
                    Name = "Category 3",
                    Description="Description Category 3"


                }
            };

        var mockRepo = new Mock<ICategoryRepository>();

        mockRepo.Setup(r => r.GetAll()).ReturnsAsync(Categories);
        mockRepo.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync((int id) =>
        {
            var result = Categories.Find(q => q.Id == id);
            if (result != null)
                return result;
            else return null;

        } );

        mockRepo.Setup(r => r.Create(It.IsAny<CategoryDTO>()))
            .Returns((CategoryDTO prod) =>
            {
                Categories.Add(prod);
                return Task.CompletedTask;
            });

        mockRepo.Setup(r => r.IsCategoryUnique(It.IsAny<string>()))
            .ReturnsAsync((string name) =>
            {
                return !Categories.Any(q => q.Name == name);
            });

        return mockRepo;
    }
}
