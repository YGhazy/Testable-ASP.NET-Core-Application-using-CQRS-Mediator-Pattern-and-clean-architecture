using AutoMapper;
using Blazor.Application.Contracts.Logging;
using Blazor.Application.Features.Categories.Commands;
using Blazor.Application.IRepository;
using Blazor.Application.Mapper;
using Blazor.Application.UnitTests.Mocks;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Application.UnitTests.Features.Categories.Fixtures
{
    public class CategoryFixture
    {
        public readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private IMapper _mapper;
        private Mock<IAppLogger<CreateCategoryCommandHandler>> _mockAppLogger;
        public CreateCategoryCommandHandler _createCategoryHandler;

        public CategoryFixture()
        {
            _mockCategoryRepository=MockCategoryRepository.GetMockCategoriesRepository();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<CategoryProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            _mockAppLogger = new Mock<IAppLogger<CreateCategoryCommandHandler>>();
            
            
            _createCategoryHandler=new CreateCategoryCommandHandler(_mapper, _mockCategoryRepository.Object);

        }

    }
}
