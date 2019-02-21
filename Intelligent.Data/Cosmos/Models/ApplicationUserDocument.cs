using Microsoft.Azure.Documents;
using Newtonsoft.Json;

namespace Intelligent.Data.Cosmos.Models
{
    class ApplicationUserDocument : Document
    {
        public static readonly string Partition = "ApplicationUsers";

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "applicationUserId")]
        public int ApplicationUserId { get; set; }

        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }

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

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "firstTime")]
        public bool? FirstTime { get; set; }

        [JsonProperty(PropertyName = "rememberMe")]
        public bool? RememeberMe { get; set; }

        [JsonProperty(PropertyName = "applicationId")]
        public int ApplicationId { get; set; }

        [JsonProperty(PropertyName = "roleId")]
        public int RoleId { get; set; }
    }
}
