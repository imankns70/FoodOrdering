using Confluent.Kafka;
using FoodOrdering.PaymentService.Api.Models;
using FoodOrdering.PaymentService.Api.Services;
using System.Text.Json;

namespace FoodOrdering.PaymentService.Api.BackgroundServices
{
    public class OrderCreatedConsumerService : BackgroundService
    {
        private readonly ILogger<OrderCreatedConsumerService> _logger;
        private readonly ConsumerConfig _config;
        private readonly IKafkaProducer _producer;

        public OrderCreatedConsumerService(ILogger<OrderCreatedConsumerService> logger, IConfiguration config, IKafkaProducer producer)
        {
            _logger = logger;
            _config = new ConsumerConfig
            {
                GroupId = "payment-service-group",
                BootstrapServers = config["Kafka:BootstrapServers"],
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            _producer = producer;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() =>
            {
                using var consumer = new ConsumerBuilder<Ignore, string>(_config).Build();
                consumer.Subscribe("order-created");

                try
                {
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        var consumeResult = consumer.Consume(stoppingToken);
                        _logger.LogInformation($"Received order event: {consumeResult.Message.Value}");

                        var orderEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(consumeResult.Message.Value);
                        // شبیه‌سازی پرداخت
                        var success = true; // یا با random یا شرط دلخواه

                        if (success)
                        {
                            var paymentEvent = new
                            {
                                orderEvent.OrderId,
                                Status = "Succeeded",
                                PaidAt = DateTime.UtcNow
                            };
                            var message = JsonSerializer.Serialize(paymentEvent);
                            _producer.ProduceAsync("payment-succeeded", message).Wait();

                            _logger.LogInformation($"✅ Payment succeeded for order {paymentEvent.OrderId}");


                        }
                        else
                        {
                            // Future: ارسال پیام شکست‌خورده
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    _logger.LogInformation("❌ Consumer stopped.");
                }
                finally
                {
                    consumer.Close();
                }
            }, stoppingToken);
        }
    }

}
