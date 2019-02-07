using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intelligent.API.Framework;
using Intelligent.Data.Generic;
using Newtonsoft.Json;

namespace Intelligent.API.Models.Response
{
    public class ImageReferenceResponse : BaseResponse
    {
        [JsonProperty("id")]
        public string ImageId { get; set; }

        [JsonProperty("tag")]
        public string ImageTag { get; set; }

        [JsonProperty("fileName")]
        public string FileName { get; set; }

        [JsonProperty("metadata")]
        public IList<MetaTag> Metadata { get; set; }

        [JsonProperty("ref")]
        public string ImageReference { get; set; }
    }
}
