using FoodOrdering.OrderingServiceApi.Models;

namespace FoodOrdering.OrderingServiceApi.Repositories
{
    public interface IOrderRepository
    {
        Task CreateAsync(Order order);
    }
}
