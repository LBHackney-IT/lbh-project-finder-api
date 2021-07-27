using System;
using ProjectFinderApi.V1.Boundary.Request;
using ProjectFinderApi.V1.Gateways.Interfaces;
using ProjectFinderApi.V1.Infrastructure;
using User = ProjectFinderApi.V1.Infrastructure.User;

namespace ProjectFinderApi.V1.Gateways
{
    public class UsersGateway : IUsersGateway
    {
        private readonly DatabaseContext _databaseContext;

        public UsersGateway(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public User CreateUser(CreateUserRequest createUserRequest)
        {
            var user = new User
            {
                Email = createUserRequest.EmailAddress,
                Name = createUserRequest.Name,
                Role = createUserRequest.Role,
            };

            _databaseContext.Users.Add(user);
            _databaseContext.SaveChanges();
            return user;
        }
    }
}
