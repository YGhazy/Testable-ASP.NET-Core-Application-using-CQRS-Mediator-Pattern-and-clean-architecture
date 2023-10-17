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
    [CollectionDefinition("CreateCategory")]
    public class CategoryFixtureCollection:ICollectionFixture<CategoryFixture>
    {

    }
}
