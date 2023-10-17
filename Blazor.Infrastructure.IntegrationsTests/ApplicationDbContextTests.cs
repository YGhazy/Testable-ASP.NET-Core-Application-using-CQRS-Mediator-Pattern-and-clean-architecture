using Blazor.Domain.Entities;
using Blazor.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Infrastructure.IntegrationsTests;

public class ApplicationDbContextTests
{
    private ApplicationDbContext _AppDatabaseContext;

    public ApplicationDbContextTests()
    {
        var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        _AppDatabaseContext = new ApplicationDbContext(dbOptions);

    }
    [Fact]

    public async void Save_SetDateCreatedValue()
    {
        // Arrange
        var product = new Product
        {
            Id = 1,
            Name = "Test product",
            Description = "Test product",
            Color="red",
            ImageUrl=""
        };

        // Act
        await _AppDatabaseContext.Products.AddAsync(product);
        await _AppDatabaseContext.SaveChangesAsync();

        // Assert
        product.DateCreated.ShouldNotBeNull();
    }

    [Fact]
    public async void Save_SetDateModifiedValue()
    {
        // Arrange
        var product = new Product
        {
            Id = 1,
            Name = "Test product",
            Description = "Test product",
            Color="red",
            ImageUrl=""
            
        };

        // Act
        await _AppDatabaseContext.Products.AddAsync(product);
        await _AppDatabaseContext.SaveChangesAsync();

        // Assert
        product.DateModified.ShouldNotBeNull();
    }
}

