using System;
using Intelligent.API.Framework;
using Intelligent.Data.Generic;
using Newtonsoft.Json;
namespace Intelligent.API.Models.Response
{
    public class MessageReferenceResponse : BaseResponse
    {
        [JsonProperty("userid")]
        public string UserId { get; set; }

        [JsonProperty("conversationid")]
        public string ConversationId { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
    }
}
