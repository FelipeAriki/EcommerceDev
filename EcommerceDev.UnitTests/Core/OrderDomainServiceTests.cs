using EcommerceDev.Core.Entities;
using EcommerceDev.Core.Repositories;
using EcommerceDev.Core.Services;
using Moq;

namespace EcommerceDev.UnitTests.Core;

public class OrderDomainServiceTests
{
    [Fact]
    public void Distance100kmAnd5Units_CalculateShippingCostIsCalled_ReturnCorrectValue()
    {
        //Arrange
        var repositoryMock = new Mock<IProductRepository>();

        const int distanceInKm = 100;
        var items = new List<OrderItem>
        {
            new(Guid.NewGuid(), 5)
        };

        // Act
        var orderDomainService = new OrderDomainService(repositoryMock.Object);
        var result = orderDomainService.CalculateShippingCost(distanceInKm, items);

        // Assert
        Assert.Equal(3_012.50m, result);
    }

    [Fact]
    public void Distance0kmAnd10Units_CalculateShippingCostIsCalled_ReturnCorrectValue()
    {
        //Arrange
        var repositoryMock = new Mock<IProductRepository>();

        const int distanceInKm = 0;
        var items = new List<OrderItem>
        {
            new(Guid.NewGuid(), 10)
        };

        //Act
        var orderDomainService = new OrderDomainService(repositoryMock.Object);
        var result = orderDomainService.CalculateShippingCost(distanceInKm, items);

        //Assert
        Assert.Equal(55, result);
    }

    [Fact]
    public void Distance1kmAnd10Units_CalculateShippingCostIsCalled_ReturnCorrectValue()
    {
        // Arrange
        var repositoryMock = new Mock<IProductRepository>();

        const int distanceInKm = 1;
        var items = new List<OrderItem>
        {
            new(Guid.NewGuid(), 10)
        };

        // Act
        var orderDomainService = new OrderDomainService(repositoryMock.Object);
        var result = orderDomainService.CalculateShippingCost(distanceInKm, items);

        // Assert
        Assert.Equal(55, result);
    }

    [Fact]
    public void Distance50kmAnd3And7Units_CalculateShippingCostIsCalled_ReturnCorrectValue()
    {
        // Arrange
        var repositoryMock = new Mock<IProductRepository>();

        const int distanceInKm = 50;
        var items = new List<OrderItem>
        {
            new(Guid.NewGuid(), 3),
            new(Guid.NewGuid(), 7)
        };

        // Act
        var orderDomainService = new OrderDomainService(repositoryMock.Object);
        var result = orderDomainService.CalculateShippingCost(distanceInKm, items);

        // Assert
        Assert.Equal(1_525m, result);
    }

    [Fact]
    public void Distance20km0Units_CalculateShippingCostIsCalled_ThrowError()
    {
        // Arrange
        var repositoryMock = new Mock<IProductRepository>();

        const int distanceInKm = 20;
        var items = new List<OrderItem>();

        // Act
        var orderDomainService = new OrderDomainService(repositoryMock.Object);
        Action action = () => orderDomainService.CalculateShippingCost(distanceInKm, items);

        // Assert
        var exception = Assert.Throws<InvalidOperationException>(action);

        Assert.Equal("No items found.", exception.Message);
    }

    [Fact]
    public void Distance500km10Units_CalculateShippingCostIsCalled_ThrowError()
    {
        // Arrange
        var repositoryMock = new Mock<IProductRepository>();

        const int distanceInKm = 500;
        var items = new List<OrderItem>
        {
            new(Guid.NewGuid(), 10)
        };

        // Act
        var orderDomainService = new OrderDomainService(repositoryMock.Object);
        Action action = () => orderDomainService.CalculateShippingCost(distanceInKm, items);

        // Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(action);

        Assert.StartsWith("Distance out of range.", exception.Message);
    }

    [Fact]
    public void DistanceMinus10And10Units_CalculateShippingCostIsCalled_ThrowError()
    {
        // Arrange
        var repositoryMock = new Mock<IProductRepository>();

        const int distanceInKm = -10;
        var items = new List<OrderItem>
        {
            new(Guid.NewGuid(), 10)
        };

        // Act
        var orderDomainService = new OrderDomainService(repositoryMock.Object);
        Action action = () => orderDomainService.CalculateShippingCost(distanceInKm, items);

        // Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(action);

        Assert.StartsWith("Distance out of range.", exception.Message);
    }
}
