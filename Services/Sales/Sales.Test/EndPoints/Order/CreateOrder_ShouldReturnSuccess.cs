using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Sales.Test.Utils;
using Sales.Application.Command.Order;
using MainTest.Framework.ApiResponse;

namespace Sales.Test.Controllers.Orders;

public class CreateOrder_ShouldReturnsSuccess : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public CreateOrder_ShouldReturnsSuccess(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task WhenValidRequest_ReturnsSuccess()
    {
        // Arrange
        var createOrderRequest = new CreateOrderCommand
        {
            CustomerName = "Tapsi",
            TotalAmount = 1000
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/Order/CreateOrder", createOrderRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var createdOrder = await response.Content.ReadFromJsonAsync<ApiResponse<Guid>>();
        createdOrder.Should().NotBeNull();
        createdOrder.HasError.Should().Be(false);
        createdOrder.Data.Should().NotBe(Guid.Empty);
    }
}
