using Confluent.Kafka;
using DebeziumPoc.Api.Application.Common.Interfaces;
using DebeziumPoc.Api.Infrastructure.Configurations;
using DebeziumPoc.Api.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace DebeziumPoc.Api.Infrastructure.Kafka
{
    public class KafkaConsumer : IEventConsumer
    {
        private readonly KafkaConfiguration _kafkaConfiguration;
        private IConsumer<string, string> _consumer;
        private readonly ILogger<KafkaConsumer> _logger;

        public KafkaConsumer(ILogger<KafkaConsumer> logger, IOptions<KafkaConfiguration> kafkaConfigurationOptions)
        {
            _logger = logger;
            _kafkaConfiguration = kafkaConfigurationOptions?.Value ?? throw new ArgumentException(nameof(kafkaConfigurationOptions));
            _consumer = CreateConsumer();
        }

        public ConsumeResult<string, string> ReadMessage(CancellationToken  cancellationToken)
        {
            return _consumer.Consume(cancellationToken);
        }

        private IConsumer<string, string> CreateConsumer()
        {
            var config = new ConsumerConfig()
            {
                BootstrapServers = _kafkaConfiguration.Brokers,
                GroupId = _kafkaConfiguration.ConsumerGroup,
                SecurityProtocol = SecurityProtocol.Plaintext,
                EnableAutoCommit = _kafkaConfiguration.AutoCommit,
                StatisticsIntervalMs = _kafkaConfiguration.StatisticsIntervalMs,
                SessionTimeoutMs = _kafkaConfiguration.SessionTimeoutMs,
                MaxPollIntervalMs = _kafkaConfiguration.MaxPollIntervalMs,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnablePartitionEof = true,
                AutoCommitIntervalMs = 5000,
                EnableAutoOffsetStore = true,
            };

            _consumer = new ConsumerBuilder<string, string>(config).SetStatisticsHandler((_, kafkaStatistics) => LogKafkaStats(kafkaStatistics)).
                SetErrorHandler((_, e) => LogKafkaError(e)).Build();
            _consumer.Subscribe(_kafkaConfiguration.Topic);

            return _consumer;
        }

        private void LogKafkaStats(string kafkaStatistics)
        {
            var stats = JsonConvert.DeserializeObject<KafkaStatistics>(kafkaStatistics);

            if (stats?.topics != null && stats.topics.Count > 0)
            {
                foreach (var topic in stats.topics)
                {
                    foreach (var partition in topic.Value.Partitions)
                    {
                        Task.Run(() =>
                        {
                            var logMessage = $"FxRates:KafkaStats Topic: {topic.Key} Partition: {partition.Key} PartitionConsumerLag: {partition.Value.ConsumerLag}";
                            _logger.LogInformation(logMessage);
                        });
                    }
                }
            }
        }

        private void LogKafkaError(Error ex)
        {
            Task.Run(() =>
            {
                var error = $"Kafka Exception: ErrorCode:[{ex.Code}] Reason:[{ex.Reason}] Message:[{ex.ToString()}]";
                _logger.LogError(error);
            });
        }

    }



}
