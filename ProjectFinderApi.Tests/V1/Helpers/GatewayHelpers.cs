using ProjectFinderApi.V1.Infrastructure;

namespace ProjectFinderApi.Tests.V1.Helpers
{
    public static class GatewayHelpers
    {
        public static User CreateUserDatabaseEntity(int id = 1, string email = "testemail@example.com", string firstName = "test-first-name", string lastName = "test-last-name", string role = "Manager")
        {
            return new User { Id = id, Email = email, FirstName = firstName, LastName = lastName, Role = role };
        }
    }
}
