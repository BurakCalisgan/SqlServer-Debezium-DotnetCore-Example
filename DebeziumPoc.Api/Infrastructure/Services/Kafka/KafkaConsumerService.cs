using DebeziumPoc.Api.Application.Common.Interfaces;
using DebeziumPoc.Api.Application.Service.Abstractions;
using DebeziumPoc.Api.Models;
using Newtonsoft.Json;

namespace DebeziumPoc.Api.Infrastructure.Services
{
    public class KafkaConsumerService : BackgroundService
    {
        private IEventConsumer _consumer;
        private readonly ILogger<KafkaConsumerService> _logger;
        private readonly IServiceProvider _collection;
        
        public KafkaConsumerService(IEventConsumer consumer, ILogger<KafkaConsumerService> logger,IServiceProvider collection)
        {
            _consumer = consumer;
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            _collection = collection;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Factory.StartNew(() =>
                    ConsumeTopic(stoppingToken),
                    stoppingToken,
                    TaskCreationOptions.LongRunning,
                    TaskScheduler.Current);
        }

        private async Task ConsumeTopic(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = _consumer.ReadMessage(stoppingToken);

                    if (consumeResult?.Message == null) continue;


                    var cdcResult = JsonConvert.DeserializeObject<CdcModel>(consumeResult.Message.Value);
                    _collection.GetService<IProductService>().Add(cdcResult);

                    _logger.LogInformation($"[{consumeResult.Message.Key}] {consumeResult.Topic} - {consumeResult.Message.Value}");
                    _logger.LogInformation($"[Kafka Info] {consumeResult.Topic} - {consumeResult.TopicPartitionOffset}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }
            }

        }


    }
}
