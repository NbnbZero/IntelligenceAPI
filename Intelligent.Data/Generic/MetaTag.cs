using Newtonsoft.Json;

namespace Intelligent.Data.Generic
{
    public class MetaTag
    {
        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }

        [JsonProperty(PropertyName = "value")]
        public object Value { get; set; }

        [JsonProperty(PropertyName = "_type")]
        public string Type { get; set; }
    }
}
