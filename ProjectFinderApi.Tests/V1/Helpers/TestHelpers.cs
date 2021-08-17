
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

        public static GetUserRequest CreateGetUserRequest(string? email = null)
        {
            return new Faker<GetUserRequest>()
            .RuleFor(u => u.EmailAddress, f => email ?? f.Person.Email);
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
    }

}
