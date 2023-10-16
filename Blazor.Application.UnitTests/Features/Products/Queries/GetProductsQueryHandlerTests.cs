using AutoMapper;
using Blazor.Application.Features.Products.Queries;
using Blazor.Application.IRepository;
using Blazor.Application.Mapper;
using Blazor.Application.UnitTests.Mocks;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Blazor.Application.Contracts.Logging;

namespace Blazor.Application.UnitTests.Features.Products.Queries
{
    public class GetProductsQueryHandlerTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private IMapper _mapper;
        private Mock<IAppLogger<GetProductsQueryHandler>> _mockAppLogger;

        public GetProductsQueryHandlerTests()
        {
            _mockProductRepository=MockProductRepository.GetMockProductsRepository();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<ProductProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            _mockAppLogger = new Mock<IAppLogger<GetProductsQueryHandler>>();

        }

        [Fact]
        public async Task GetProductListTest()
        {

            var handler = new GetProductsQueryHandler(_mapper, _mockProductRepository.Object, _mockAppLogger.Object);
            var result = await handler.Handle(new GetProductsQuery(), CancellationToken.None);

            result.ShouldBeOfType<List<ProductDTO>>();

            result.Count.ShouldBe(4);
        }
    }
}
