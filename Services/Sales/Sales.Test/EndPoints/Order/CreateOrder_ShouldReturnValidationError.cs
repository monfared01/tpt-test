using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;
using Sales.Api;
using Sales.Application;
using Sales.Test.Utils;
using Sales.Application.Command.Order;
using MainTest.Framework.ApiResponse;

namespace Sales.Test.Controllers.Orders;

public class CreateOrder_ShouldReturnValidationError : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public CreateOrder_ShouldReturnValidationError(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task WhenValidRequest_ReturnsValidationError()
    {
        // Arrange
        var createOrderRequest = new CreateOrderCommand
        {
            CustomerName = "Tapsi",
            TotalAmount = 0
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/Order/CreateOrder", createOrderRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var createdOrder = await response.Content.ReadFromJsonAsync<ApiResponse>();
        createdOrder.Should().NotBeNull();
        createdOrder.HasError.Should().Be(true);
        createdOrder.Message.Should().Be("One or more validation errors occurred.");
    }
}
