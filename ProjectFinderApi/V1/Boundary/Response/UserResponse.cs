using System;

namespace ProjectFinderApi.V1.Boundary.Response
{
    public class UserResponse
    {
        public int Id { get; set; }

        public string Email { get; set; }
        public string Name { get; set; }

        public string Role { get; set; }

    }
}