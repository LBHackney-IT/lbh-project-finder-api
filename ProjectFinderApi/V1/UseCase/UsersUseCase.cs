using System;
using ProjectFinderApi.V1.Boundary.Request;
using ProjectFinderApi.V1.Boundary.Response;
using ProjectFinderApi.V1.Gateways.Interfaces;
using ProjectFinderApi.V1.UseCase.Interfaces;
using ProjectFinderApi.V1.Factories;

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
            return _usersGateway.CreateUser(createUserRequest).ToDomain().ToResponse();
        }
    }
}
