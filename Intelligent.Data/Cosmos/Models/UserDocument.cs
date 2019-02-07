using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;

namespace Intelligent.Data.Cosmos.Models
{
    class UserDocument : Document
    {
        public static readonly string Partition = "Users";

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public int UserId { get; set; }

        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "secondaryEmail")]
        public string SecondaryEmail { get; set; }

        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "secondaryPhone")]
        public string SecondaryPhone { get; set; }

        [JsonProperty(PropertyName = "street")]
        public string Street { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "postalCode")]
        public string PostalCode { get; set; }

        [JsonProperty(PropertyName = "accountId")]
        public int AccountId { get; set; }

        [JsonProperty(PropertyName = "roleId")]
        public int RoleId { get; set; }
    }
}
