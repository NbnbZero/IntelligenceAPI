using System;
using System.Collections.Generic;
using System.Text;
using Intelligent.Data.Generic;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;

namespace Intelligent.Data.Cosmos.Models
{
    public class UploadFileDocument : Document
    {
        public static readonly string Partition = "Uploads";

        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "documentId")]
        public string DocumentId { get; set; }

        [JsonProperty(PropertyName = "fileName")]
        public string FileName { get; set; }

        [JsonProperty(PropertyName = "contentType")]
        public string ContentType { get; set; }

        [JsonProperty(PropertyName = "uri")]
        public Uri Reference { get; set; }

        [JsonProperty(PropertyName = "_metadata")]
        public IList<MetaTag> Metadata { get; set; }
    }
}
