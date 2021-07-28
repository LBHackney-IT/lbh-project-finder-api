using System.Collections.Generic;
using System.Linq;
using ProjectFinderApi.V1.Boundary.Response;
using ProjectFinderApi.V1.Domain;

namespace ProjectFinderApi.V1.Factories
{
    public static class ResponseFactory
    {
        public static UserResponse ToResponse(this Domain.User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role,
            };
        }

    }
}
