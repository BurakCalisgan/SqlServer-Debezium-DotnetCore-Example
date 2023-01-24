﻿using Newtonsoft.Json;

namespace DebeziumPoc.Api.Models
{
    public class KafkaStatistics
    {
        /// <summary>
        ///     Handle instance name.
        /// </summary>
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("topics")]
        public IReadOnlyDictionary<string, TopicData> topics { get; set; }

    }
}