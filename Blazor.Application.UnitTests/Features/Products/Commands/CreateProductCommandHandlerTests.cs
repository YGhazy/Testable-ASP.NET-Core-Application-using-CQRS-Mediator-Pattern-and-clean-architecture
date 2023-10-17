using AutoMapper;
using Blazor.Application.Contracts.Logging;
using Blazor.Application.Features.Products.Commands.CreateProductCommand;
using Blazor.Application.Features.Products.Queries;
using Blazor.Application.IRepository;
using Blazor.Application.Mapper;
using Blazor.Application.Unit_of_work;
using Blazor.Application.UnitTests.Mocks;
using Blazor.Domain.Common;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Application.UnitTests.Features.Products.Commands;



public class CreateProductCommandHandlerTests
{
    private readonly Mock<IProductRepository> _mockProductRepository;
    private readonly Mock<ICategoryRepository> _mockCategoryRepository;
    private IMapper _mapper;
    private Mock<IAppLogger<CreateProductCommandHandler>> _mockAppLogger;

    public CreateProductCommandHandlerTests()
    {
        _mockProductRepository=MockProductRepository.GetMockProductsRepository();
        _mockCategoryRepository=MockCategoryRepository.GetMockCategoriesRepository();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<ProductProfile>();
        });
        _mapper = mapperConfig.CreateMapper();
        _mockAppLogger = new Mock<IAppLogger<CreateProductCommandHandler>>();

    }

    [Theory]
    [Trait("Product","create")]
    [InlineData("Test", "Description", "red", 2)]
    [InlineData("Test", "Description", "red", 0)]
    [InlineData("Test", "descriptionn", null, 1)]
    [InlineData(null, null, "white", 2)]
    [InlineData(null, null, "black", 2)]
    [InlineData(null, null, "", 2)]
    public async Task CreateProductTest(string name, string description, string Color, int CategoryId)
    {
        var handler = new CreateProductCommandHandler(_mapper, _mockProductRepository.Object, _mockCategoryRepository.Object, _mockAppLogger.Object);
        var result = await handler.Handle(new CreateProductCommand()
        {
            Name =name,
            Description = description,
            Color=Color,
            CategoryId=CategoryId,
        }, CancellationToken.None);

        var products = await _mockProductRepository.Object.GetAll();
        products.ToList().Count.ShouldBe(4);
    }


    [Fact]
    [Trait("Product", "create")]
    public async Task Handle_ValidProduct()
    {
        var handler = new CreateProductCommandHandler(_mapper, _mockProductRepository.Object, _mockCategoryRepository.Object, _mockAppLogger.Object);

        await handler.Handle(new CreateProductCommand()
        {
            Name = "Test1",
            Description = "descriptionn",
            Color="red",
            CategoryId=2,
            ImageUrl=""
        }, CancellationToken.None);

        var products = await _mockProductRepository.Object.GetAll();
        products.ToList().Count.ShouldBe(4);
    }
}
