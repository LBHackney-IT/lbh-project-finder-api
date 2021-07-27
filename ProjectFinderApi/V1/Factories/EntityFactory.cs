using ProjectFinderApi.V1.Domain;
using ProjectFinderApi.V1.Infrastructure;
using DbUser = ProjectFinderApi.V1.Infrastructure.User;
using User = ProjectFinderApi.V1.Domain.User;

namespace ProjectFinderApi.V1.Factories
{
    public static class EntityFactory
    {
        public static Entity ToDomain(this DatabaseEntity databaseEntity)
        {
            //TODO: Map the rest of the fields in the domain object.
            // More information on this can be found here https://github.com/LBHackney-IT/lbh-project-finder-api/wiki/Factory-object-mappings

            return new Entity
            {
                Id = databaseEntity.Id,
                CreatedAt = databaseEntity.CreatedAt
            };
        }

        public static User ToDomain(this DbUser user)
        {
            return new User
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Role = user.Role,

            };
        }

        public static DatabaseEntity ToDatabase(this Entity entity)
        {
            //TODO: Map the rest of the fields in the database object.

            return new DatabaseEntity
            {
                Id = entity.Id,
                CreatedAt = entity.CreatedAt
            };
        }
    }
}
