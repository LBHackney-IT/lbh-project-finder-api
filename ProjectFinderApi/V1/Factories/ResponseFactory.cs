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

        public static ProjectResponse ToResponse(this Domain.Project project)
        {
            return new ProjectResponse
            {
                Id = project.Id,
                ProjectName = project.ProjectName,
                Description = project.Description,
                ProjectContact = project.ProjectContact,
                Phase = project.Phase,
                Size = project.Size,
                Category = project.Category,
                Priority = project.Priority,
                ProductUsers = project.ProductUsers,
                Dependencies = project.Dependencies
            };
        }

    }
}
