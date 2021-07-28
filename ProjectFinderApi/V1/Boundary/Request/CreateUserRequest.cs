using System;
using System.Text.Json.Serialization;

namespace ProjectFinderApi.V1.Boundary.Request
{
    public class CreateUserRequest
    {
        [JsonPropertyName("emailAdress")]
        public string EmailAddress { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }
    }
}
