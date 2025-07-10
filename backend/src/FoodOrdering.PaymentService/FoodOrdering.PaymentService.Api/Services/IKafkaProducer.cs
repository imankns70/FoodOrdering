namespace FoodOrdering.PaymentService.Api.Services
{
    public interface IKafkaProducer
    {

        Task ProduceAsync<TValue>(string topic, TValue message);
    }
}
