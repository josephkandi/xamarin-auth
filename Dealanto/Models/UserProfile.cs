using System;
using Newtonsoft.Json;

namespace Dealanto.Models
{
    public class UserProfile
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("verified_email")]
        public bool IsEmailVerified { get; set; }

        [JsonProperty("name")]
        public string FullName { get; set; }

        [JsonProperty("given_name")]
        public string FirstName { get; set; }

        [JsonProperty("family_name")]
        public string LastName { get; set; }

        [JsonProperty("link")]
        public string ProfileUrl { get; set; }

        [JsonProperty("picture")]
        public string PictureUrl { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }
    }
}
