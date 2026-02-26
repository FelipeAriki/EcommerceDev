using EcommerceDev.Application.Queries.Products.GetAllProducts;
using EcommerceDev.Core.Entities;
using EcommerceDev.Core.Repositories;
using EcommerceDev.Infrastructure.Caching;
using Moq;

namespace EcommerceDev.UnitTests.Application;

public class GetAllProductsQueryTests
{
    [Fact]
    public async Task ThreeProductsNotInCache_GetAllProductsIsCalled_ReturnCorrectValues()
    {
        // Arrange
        var productRepositoryMock = new Mock<IProductRepository>();
        var cacheServiceMock = new Mock<ICacheService>();

        var items = new List<Product>
        {
            new("Product A", "Description A", 1, "Brand A", 1, Guid.NewGuid()),
            new("Product B", "Description B", 1, "Brand B", 2, Guid.NewGuid()),
            new("Product C", "Description C", 1, "Brand C", 3, Guid.NewGuid())
        };

        productRepositoryMock.Setup(pr => pr.GetProductsAsync()).ReturnsAsync(items);

        // Act
        var query = new GetAllProductsQuery();
        var handler = new GetAllProductsQueryHandler(productRepositoryMock.Object, cacheServiceMock.Object);

        var result = await handler.HandleAsync(query);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.NotEmpty(result.Data);
        cacheServiceMock.Verify(pr => pr.GetAsync<List<GetAllProductsItemViewModel>>(It.IsAny<string>()), Times.Once);
        productRepositoryMock.Verify(pr => pr.GetProductsAsync(), Times.Once);
    }

    [Fact]
    public async Task ThreeProductsInCache_GetAllProductsIsCalled_ReturnCorrectValues()
    {
        // Arrange
        var productRepositoryMock = new Mock<IProductRepository>();
        var cacheServiceMock = new Mock<ICacheService>();

        var items = new List<GetAllProductsItemViewModel>
        {
            new() { Id = Guid.NewGuid(), Title = "Product A", Price = 1 },
            new() { Id = Guid.NewGuid(), Title = "Product B", Price = 2 },
            new() { Id = Guid.NewGuid(), Title = "Product C", Price = 3 }
        };

        cacheServiceMock.Setup(pr =>
            pr.GetAsync<List<GetAllProductsItemViewModel>>(It.IsAny<string>()))
            .ReturnsAsync(items);

        // Act
        var query = new GetAllProductsQuery();
        var handler = new GetAllProductsQueryHandler(productRepositoryMock.Object, cacheServiceMock.Object);

        var result = await handler.HandleAsync(query);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.NotEmpty(result.Data);
        cacheServiceMock.Verify(pr => pr.GetAsync<List<GetAllProductsItemViewModel>>(It.IsAny<string>()), Times.Once);
        productRepositoryMock.Verify(pr => pr.GetProductsAsync(), Times.Never);
    }
}
