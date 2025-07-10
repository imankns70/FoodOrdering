using Confluent.Kafka;
using FoodOrdering.PaymentService.Api.Services;
using System.Text.Json;

public class KafkaProducer : IKafkaProducer
{
    private readonly IProducer<Null, string> _producer;

    public KafkaProducer(IConfiguration configuration)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = configuration["Kafka:BootstrapServers"]
        };

        _producer = new ProducerBuilder<Null, string>(config).Build();
    }

    public async Task ProduceAsync<TValue>(string topic, TValue message)
    {
        var json = JsonSerializer.Serialize(message);

        var kafkaMessage = new Message<Null, string> { Value = json };

        await _producer.ProduceAsync(topic, kafkaMessage);
    }
}
