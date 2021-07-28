using ProjectFinderApi.V1.Domain;
using ProjectFinderApi.V1.Infrastructure;
using DbUser = ProjectFinderApi.V1.Infrastructure.User;
using User = ProjectFinderApi.V1.Domain.User;

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
    }
}
