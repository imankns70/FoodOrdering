using Confluent.Kafka;

namespace FoodOrdering.NotificationService.Api.BackgroundServices
{
    public class PaymentSucceededConsumerService : BackgroundService
    {
        private readonly ILogger<PaymentSucceededConsumerService> _logger;
        private readonly ConsumerConfig _config;

        public PaymentSucceededConsumerService(ILogger<PaymentSucceededConsumerService> logger, IConfiguration config)
        {
            _logger = logger;
            _config = new ConsumerConfig
            {
                GroupId = "notification-service-group",
                BootstrapServers = config["Kafka:BootstrapServers"],
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() =>
            {
                using var consumer = new ConsumerBuilder<Ignore, string>(_config).Build();
                consumer.Subscribe("payment-succeeded");

                try
                {
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        var result = consumer.Consume(stoppingToken);
                        _logger.LogInformation($"✅ Payment Event Received: {result.Message.Value}");

                        // TODO: ارسال ایمیل یا نوتیفیکیشن
                    }
                }
                catch (OperationCanceledException) { }
                finally
                {
                    consumer.Close();
                }
            }, stoppingToken);
        }
    }

}
