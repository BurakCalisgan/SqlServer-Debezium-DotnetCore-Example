namespace DebeziumPoc.Api.Infrastructure.Configurations
{
    public class KafkaConfiguration
    {
        public string Brokers { get; set; }
        public string ConsumerGroup { get; set; }
        public string Topic { get; set; }
        public int StatisticsIntervalMs { get; set; }
        public int SessionTimeoutMs { get; set; }
        public bool AutoCommit { get; set; }
        public int MaxPollIntervalMs { get; set; }
        public int RecopilationTimeMs { get; set; }
        public int MaxItemsDequeue { get; set; }
        public int ConsumeTimeoutMs { get; set; }
    }
}
