using FoodOrdering.OrderingServiceApi.Models;
using MongoDB.Driver;

namespace FoodOrdering.OrderingServiceApi.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _orders;

        public OrderRepository(IConfiguration config)
        {
            var client = new MongoClient(config["Mongo:ConnectionString"]);
            var database = client.GetDatabase(config["Mongo:Database"]);
            _orders = database.GetCollection<Order>("orders");
        }

        public async Task CreateAsync(Order order)
        {
            await _orders.InsertOneAsync(order);


        }

        // می‌شه روش‌های دیگه مثل GetById و GetAll رو بعداً اضافه کرد
    }
}
