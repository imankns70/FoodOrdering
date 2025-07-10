using FoodOrdering.OrderingService.Api.Controllers;
using FoodOrdering.OrderingService.Api.Services;
using FoodOrdering.OrderingServiceApi.Models;
using FoodOrdering.OrderingServiceApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FoodOrdering.OrderingService.Tests.Controllers
{
    public class OrderControllerTests
    {
        public async Task CreateOrder_Should_Return_Ok()
        {
            var mockRepo = new Mock<IOrderRepository>();
            var mockProducer = new Mock<IKafkaProducer>();
            var orderRequest = new Order
            {
                UserId = "user-1",
                CreatedAt = DateTime.Now,
                Items = new List<OrderItem>()
                {
                        new OrderItem { ProductId = "p1", Quantity = 2 }
                }
            };
            var createdOrder = new Order
            {
                Id = "abc123",
                UserId = orderRequest.UserId,
                Items = orderRequest.Items
            };

            // شبیه‌سازی رفتار ریپازیتوری
            mockRepo.Setup(r => r.CreateAsync(It.IsAny<Order>()))
                .Returns(Task.CompletedTask);

            // شبیه‌سازی رفتار KafkaProducer
            mockProducer.Setup(r => r.PublishOrderCreatedAsync(It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var controller = new OrdersController(mockRepo.Object, mockProducer.Object);

            // Act
            var result = await controller.Create(orderRequest);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedOrder = Assert.IsType<Order>(okResult.Value);
            Assert.Equal("abc123", returnedOrder.Id);
        }
    }
}
