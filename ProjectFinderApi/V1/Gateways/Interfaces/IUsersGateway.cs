using System;
using System.Collections.Generic;
using ProjectFinderApi.V1.Boundary.Request;
using ProjectFinderApi.V1.Boundary.Response;
using ProjectFinderApi.V1.Infrastructure;

namespace ProjectFinderApi.V1.Gateways.Interfaces
{
    public interface IUsersGateway
    {
        User CreateUser(CreateUserRequest createUserRequest);

        IEnumerable<User> GetUsers();

        User GetUserByEmail(string email);
    }
}
