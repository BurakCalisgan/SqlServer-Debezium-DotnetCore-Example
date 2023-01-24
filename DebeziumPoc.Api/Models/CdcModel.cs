using Newtonsoft.Json;

namespace DebeziumPoc.Api.Models
{
    public class CdcModel
    {
        [JsonProperty("schema")]
        public CdcSchema Schema { get; set; }
        [JsonProperty("payload")]
        public CdcPayload Payload { get; set; }
    }
}
