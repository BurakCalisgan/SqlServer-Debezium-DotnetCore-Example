using Newtonsoft.Json;

namespace DebeziumPoc.Api.Models
{
    public class CdcSchema
    {
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
