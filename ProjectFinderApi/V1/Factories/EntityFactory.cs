using ProjectFinderApi.V1.Domain;
using ProjectFinderApi.V1.Infrastructure;
using DbUser = ProjectFinderApi.V1.Infrastructure.User;
using User = ProjectFinderApi.V1.Domain.User;
using DbProject = ProjectFinderApi.V1.Infrastructure.Project;
using Project = ProjectFinderApi.V1.Domain.Project;

namespace ProjectFinderApi.V1.Factories
{
    public static class EntityFactory
    {

        public static User ToDomain(this DbUser user)
        {
            return new User
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,

            };
        }

        public static Project ToDomain(this DbProject project)
        {
            return new Project
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
