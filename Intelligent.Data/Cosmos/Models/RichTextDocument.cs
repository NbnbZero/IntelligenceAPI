using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;

namespace Intelligent.Data.Cosmos.Models
{
    class RichTextDocument : Document
    {
        public static readonly string Partition = "RichTextFiles";

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "fileId")]
        public int FileId { get; set; }

        [JsonProperty(PropertyName = "filename")]
        public string Filename { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public int UserId { get; set; }

        [JsonProperty(PropertyName = "applicationId")]
        public int ApplicationId { get; set; }
    }
}
