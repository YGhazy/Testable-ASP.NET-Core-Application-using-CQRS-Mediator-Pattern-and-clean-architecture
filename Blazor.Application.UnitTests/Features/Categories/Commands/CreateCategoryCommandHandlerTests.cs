using AutoMapper;
using Blazor.Application.Contracts.Logging;
using Blazor.Application.Features.Categories.Commands;
using Blazor.Application.IRepository;
using Blazor.Application.Mapper;
using Blazor.Application.UnitTests.Features.Categories.Fixtures;
using Blazor.Application.UnitTests.Mocks;
using Features.Categories.Commands;
using Features.Categories.Queries.GetCategories;
using Moq;
using Shouldly;

namespace Blazor.Application.UnitTests.Features.Categories.Commands;

[Collection("CreateCategory")]
public class CreateCategoryCommandHandlerTests
{
    private readonly CategoryFixture categoryFixture;
    public CreateCategoryCommandHandlerTests(CategoryFixture _categoryFixture)
    {
        categoryFixture=_categoryFixture;
    }

    [Theory]
    [Trait("Category", "Create")]
    [InlineData("Category 1", "Description")]
    [InlineData("Test", null)]
    [InlineData(null, "Description")]
    [InlineData(null, null)]

    public async Task CreateCategoryTest(string name, string description)
    {
        var handler = categoryFixture._createCategoryHandler;
        var result = await handler.Handle(new CreateCategoryCommand()
        {
            Name =name,
            Description = description
        }, CancellationToken.None);

        var products = await categoryFixture._mockCategoryRepository.Object.GetAll();
        products.ToList().Count.ShouldBe(4);

        result.ShouldBeOfType<int>();

    }


    [Fact]
    [Trait("Category", "Create")]

    public async Task Handle_ValidCategory()
    {
        var handler = categoryFixture._createCategoryHandler;

        await handler.Handle(new CreateCategoryCommand()
        {
            Name = "Test1",
            Description = "descriptionn"
        }, CancellationToken.None);

        var products = await categoryFixture._mockCategoryRepository.Object.GetAll();
        products.ToList().Count.ShouldBe(4);
    }
}
