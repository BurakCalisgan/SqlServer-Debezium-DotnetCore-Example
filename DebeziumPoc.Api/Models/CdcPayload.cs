using Newtonsoft.Json;

namespace DebeziumPoc.Api.Models
{
    public class CdcPayload
    {
        [JsonProperty("op")]
        public string op { get; set; }
        [JsonProperty("before")]
        public CdcProduct Before { get; set; }
        [JsonProperty("after")]
        public CdcProduct After { get; set; }
    }
}
