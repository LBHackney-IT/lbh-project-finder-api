using System;

namespace ProjectFinderApi.V1.Domain
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Role { get; set; }
    }
}
