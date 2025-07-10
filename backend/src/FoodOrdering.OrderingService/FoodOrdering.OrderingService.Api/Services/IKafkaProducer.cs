namespace FoodOrdering.OrderingService.Api.Services
{
    public interface IKafkaProducer
    {

        Task PublishOrderCreatedAsync(object evt);
    }
}
