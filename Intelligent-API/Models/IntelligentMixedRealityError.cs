using Intelligent.API.Framework;
using Newtonsoft.Json;

namespace Intelligent.API.Models
{
    public class IntelligentMixedRealityError : BaseResponse
    {
        [JsonProperty(PropertyName = "code")]
        public string ErrorCode { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}
