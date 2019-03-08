using System;
using Newtonsoft.Json;

namespace Intelligent.API.Data
{
    public class Message
    {
        [JsonProperty(PropertyName = "to")]
        public string ToUser { get; set; }

        [JsonProperty(PropertyName = "from")]
        public string FromUser { get; set; }

        [JsonProperty(PropertyName = "timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty(PropertyName = "body")]
        public string Body { get; set; }
    }
}
