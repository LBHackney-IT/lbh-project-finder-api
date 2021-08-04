using System;

namespace ProjectFinderApi.V1.Domain
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Role { get; set; } = null!;
    }
}
