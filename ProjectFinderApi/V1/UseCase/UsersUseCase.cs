using System;
using ProjectFinderApi.V1.Boundary.Request;
using ProjectFinderApi.V1.Boundary.Response;
using ProjectFinderApi.V1.Gateways.Interfaces;
using ProjectFinderApi.V1.UseCase.Interfaces;
using ProjectFinderApi.V1.Factories;
using System.Collections.Generic;
using System.Linq;

namespace ProjectFinderApi.V1.UseCase
{
    public class UsersUseCase : IUsersUseCase
    {
        private readonly IUsersGateway _usersGateway;

        public UsersUseCase(IUsersGateway usersGateway)
        {
            _usersGateway = usersGateway;
        }

        public UserResponse ExecutePost(CreateUserRequest createUserRequest)
        {
            // Check if user exists first, return error if so

            return _usersGateway.CreateUser(createUserRequest).ToDomain().ToResponse();
        }

        public List<UserResponse> ExecuteGetAll()
        {

            var dbUsers = _usersGateway.GetUsers();

            var domainUsers = dbUsers.Select(x => x.ToDomain().ToResponse()).ToList();

            return domainUsers;

        }
    }
}
