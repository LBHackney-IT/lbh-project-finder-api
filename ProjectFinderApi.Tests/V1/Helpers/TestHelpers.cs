
using Bogus;
using ProjectFinderApi.V1.Boundary.Request;
using ProjectFinderApi.V1.Boundary.Response;
using ProjectFinderApi.V1.Infrastructure;

#nullable enable
namespace ProjectFinderApi.Tests.V1.Helpers
{
    public static class TestHelpers
    {
        public static CreateUserRequest CreateUserRequest(string? email = null, string? firstName = null, string? lastName = null, string? role = null)
        {
            return new Faker<CreateUserRequest>()
            .RuleFor(u => u.EmailAddress, f => email ?? f.Person.Email)
            .RuleFor(u => u.FirstName, f => firstName ?? f.Person.FirstName)
            .RuleFor(u => u.LastName, f => lastName ?? f.Person.LastName)
            .RuleFor(u => u.Role, f => role ?? f.Random.String2(70));
        }

        public static User CreateUser(
            int? id = null,
            string? firstName = null,
            string? lastName = null,
            string? email = null,
            string? role = null)
        {
            return new Faker<User>()
            .RuleFor(w => w.Id, f => id ?? f.UniqueIndex + 1)
            .RuleFor(u => u.Email, f => email ?? f.Person.Email)
            .RuleFor(u => u.FirstName, f => firstName ?? f.Person.FirstName)
            .RuleFor(u => u.LastName, f => lastName ?? f.Person.LastName)
            .RuleFor(u => u.Role, f => role ?? f.Random.String2(70));
        }

        public static UserResponse CreateUserResponse(
            int? id = null,
            string? firstName = null,
            string? lastName = null,
            string? email = null,
            string? role = null)
        {
            return new Faker<UserResponse>()
            .RuleFor(w => w.Id, f => id ?? f.UniqueIndex)
            .RuleFor(u => u.Email, f => email ?? f.Person.Email)
            .RuleFor(u => u.FirstName, f => firstName ?? f.Person.FirstName)
            .RuleFor(u => u.LastName, f => lastName ?? f.Person.LastName)
            .RuleFor(u => u.Role, f => role ?? f.Random.String2(200));
        }

        public static CreateProjectRequest CreateProjectRequest(
            string? projectName = null,
            string? description = null,
            string? projectContact = null,
            string? phase = null,
            string? size = null,
            string? category = null,
            string? priority = null,
            string? productUsers = null,
            string? dependencies = null)
        {
            return new Faker<CreateProjectRequest>()
            .RuleFor(p => p.ProjectName, f => projectName ?? f.Random.String2(500))
            .RuleFor(p => p.Description, f => description ?? f.Random.String2(1000))
            .RuleFor(p => p.ProjectContact, f => projectContact)
            .RuleFor(p => p.Phase, f => phase ?? f.Random.String2(70))
            .RuleFor(p => p.Size, f => size ?? f.Random.String2(70))
            .RuleFor(p => p.Category, f => category)
            .RuleFor(p => p.Priority, f => priority)
            .RuleFor(p => p.ProductUsers, f => productUsers)
            .RuleFor(p => p.Dependencies, f => dependencies);
        }

        public static Project CreateProject(
            int? id = null,
            string? projectName = null,
            string? description = null,
            string? projectContact = null,
            string? phase = null,
            string? size = null,
            string? category = null,
            string? priority = null,
            string? productUsers = null,
            string? dependencies = null)
        {
            return new Faker<Project>()
            .RuleFor(p => p.Id, f => id ?? f.UniqueIndex)
            .RuleFor(p => p.ProjectName, f => projectName ?? f.Random.String2(500))
            .RuleFor(p => p.Description, f => description ?? f.Random.String2(1000))
            .RuleFor(p => p.ProjectContact, f => projectContact)
            .RuleFor(p => p.Phase, f => phase ?? f.Random.String2(70))
            .RuleFor(p => p.Size, f => size ?? f.Random.String2(70))
            .RuleFor(p => p.Category, f => category)
            .RuleFor(p => p.Priority, f => priority)
            .RuleFor(p => p.ProductUsers, f => productUsers)
            .RuleFor(p => p.Dependencies, f => dependencies);
        }

        public static ProjectResponse CreateProjectResponse(
            int? id = null,
            string? projectName = null,
            string? description = null,
            string? projectContact = null,
            string? phase = null,
            string? size = null,
            string? category = null,
            string? priority = null,
            string? productUsers = null,
            string? dependencies = null)
        {
            return new Faker<ProjectResponse>()
            .RuleFor(p => p.Id, f => id ?? f.UniqueIndex)
            .RuleFor(p => p.ProjectName, f => projectName ?? f.Random.String2(500))
            .RuleFor(p => p.Description, f => description ?? f.Random.String2(1000))
            .RuleFor(p => p.ProjectContact, f => projectContact)
            .RuleFor(p => p.Phase, f => phase ?? f.Random.String2(70))
            .RuleFor(p => p.Size, f => size ?? f.Random.String2(70))
            .RuleFor(p => p.Category, f => category)
            .RuleFor(p => p.Priority, f => priority)
            .RuleFor(p => p.ProductUsers, f => productUsers)
            .RuleFor(p => p.Dependencies, f => dependencies);
        }

        public static GetProjectRequest GetProjectRequest(int? id = null)
        {
            return new Faker<GetProjectRequest>()
            .RuleFor(p => p.Id, f => id ?? f.UniqueIndex);
        }

        public static UpdateProjectRequest UpdateProjectRequest(
            int? id = null,
            string? projectName = null,
            string? description = null,
            string? projectContact = null,
            string? phase = null,
            string? size = null,
            string? category = null,
            string? priority = null,
            string? productUsers = null,
            string? dependencies = null)
        {
            return new Faker<UpdateProjectRequest>()
            .RuleFor(p => p.Id, f => id ?? f.UniqueIndex)
            .RuleFor(p => p.ProjectName, f => projectName ?? f.Random.String2(500))
            .RuleFor(p => p.Description, f => description ?? f.Random.String2(1000))
            .RuleFor(p => p.ProjectContact, f => projectContact)
            .RuleFor(p => p.Phase, f => phase ?? f.Random.String2(70))
            .RuleFor(p => p.Size, f => size ?? f.Random.String2(70))
            .RuleFor(p => p.Category, f => category)
            .RuleFor(p => p.Priority, f => priority)
            .RuleFor(p => p.ProductUsers, f => productUsers)
            .RuleFor(p => p.Dependencies, f => dependencies);
        }
    }

}
