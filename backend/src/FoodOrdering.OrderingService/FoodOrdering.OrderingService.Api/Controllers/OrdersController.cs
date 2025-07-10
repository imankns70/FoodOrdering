using FoodOrdering.OrderingService.Api.Services;
using FoodOrdering.OrderingServiceApi.Models;
using FoodOrdering.OrderingServiceApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.OrderingService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _repository;
        private readonly IKafkaProducer _producer;

        public OrdersController(IOrderRepository repository, IKafkaProducer producer)
        {
            _repository = repository;
            _producer = producer;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            await _repository.CreateAsync(order);

            var evt = new
            {
                order.Id,
                order.UserId,
                order.Items,
                order.CreatedAt
            };

            await _producer.PublishOrderCreatedAsync(evt);

            return Ok(order);
        }
    }
}
