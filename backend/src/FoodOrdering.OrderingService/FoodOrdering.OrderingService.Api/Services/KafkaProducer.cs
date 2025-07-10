using Confluent.Kafka;
using FoodOrdering.OrderingService.Api.Services;
using System.Text.Json;

namespace FoodOrdering.OrderingService.Services
{
    public class KafkaProducer : IKafkaProducer
    {
        private readonly IProducer<Null, string> _producer;
        private readonly string _topic;

        public KafkaProducer(IConfiguration config)
        {
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = config["Kafka:BootstrapServers"]
            };

            _producer = new ProducerBuilder<Null, string>(producerConfig).Build();
            _topic = config["Kafka:OrderCreatedTopic"];
        }

        public async Task PublishOrderCreatedAsync(object evt)
        {
            var message = JsonSerializer.Serialize(evt);
            await _producer.ProduceAsync(_topic, new Message<Null, string> { Value = message });
        }
    }
}
