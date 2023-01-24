using Newtonsoft.Json;

namespace DebeziumPoc.Api.Models
{
    public class PartitionData
    {
        [JsonProperty("partition")]
        public int Partition { get; set; }

        [JsonProperty("consumer_lag")]
        public int ConsumerLag { get; set; }
    }
}