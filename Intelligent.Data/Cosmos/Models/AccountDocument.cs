using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;

namespace Intelligent.Data.Cosmos.Models
{
    class AccountDocument : Document
    {
        public static readonly string Partition = "Accounts";

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "accountId")]
        public int AccountId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "street")]
        public string Street { get; set; }

        [JsonProperty(PropertyName = "cityId")]
        public int CityId { get; set; }

        [JsonProperty(PropertyName = "stateId")]
        public int StateId { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "postalCodeId")]
        public int PostalCodeId { get; set; }

        [JsonProperty(PropertyName = "isActive")]
        public bool IsActive { get; set; }

        [JsonProperty(PropertyName = "accountUrl")]
        public string AccountUrl { get; set; }

        [JsonProperty(PropertyName = "typeId")]
        public int TypeId { get; set; }
    }
}
