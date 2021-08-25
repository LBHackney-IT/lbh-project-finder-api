using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using ProjectFinderApi.Tests.V1.Helpers;
using ProjectFinderApi.V1.Boundary.Request;
using ProjectFinderApi.V1.Boundary.Response;
using ProjectFinderApi.V1.Exceptions;
using ProjectFinderApi.V1.Factories;
using ProjectFinderApi.V1.Gateways.Interfaces;
using ProjectFinderApi.V1.UseCase;
using ProjectFinderApi.V1.UseCase.Interfaces;
using dbUser = ProjectFinderApi.V1.Infrastructure.User;

namespace ProjectFinderApi.Tests.V1.UseCase
{
    [TestFixture]
    public class UsersUseCaseTests
    {
        private Mock<IUsersGateway> _mockUsersGateway;

        private IUsersUseCase _usersUseCase;

        private readonly Fixture _fixture = new Fixture();

        [SetUp]
        public void SetUp()
        {
            _mockUsersGateway = new Mock<IUsersGateway>();
            _usersUseCase = new UsersUseCase(_mockUsersGateway.Object);
        }

        [Test]
        public void ExecutePostCallsUsersGateway()
        {
            var userRequest = TestHelpers.CreateUserRequest();
            var user = TestHelpers.CreateUser(firstName: userRequest.FirstName, lastName: userRequest.LastName, email: userRequest.EmailAddress, role: userRequest.Role);
            _mockUsersGateway.Setup(x => x.CreateUser(userRequest)).Returns(user);

            var response = _usersUseCase.ExecutePost(userRequest);

            _mockUsersGateway.Verify(x => x.CreateUser(userRequest));
            _mockUsersGateway.Verify(x => x.CreateUser(It.Is<CreateUserRequest>(u => u == userRequest)), Times.Once());

        }

        [Test]
        public void ExecutePostReturnsCreatedUser()
        {
            var userRequest = TestHelpers.CreateUserRequest();
            var user = TestHelpers.CreateUser(firstName: userRequest.FirstName, lastName: userRequest.LastName, email: userRequest.EmailAddress, role: userRequest.Role);
            _mockUsersGateway.Setup(x => x.CreateUser(userRequest)).Returns(user);

            var response = _usersUseCase.ExecutePost(userRequest);

            response.Should().BeEquivalentTo(user.ToDomain().ToResponse());

        }

        [Test]
        public void ExecutePostThrowsExceptionIfUserAlreadyExists()
        {
            var userRequest = TestHelpers.CreateUserRequest();
            var user = TestHelpers.CreateUser(email: userRequest.EmailAddress);
            _mockUsersGateway.Setup(x => x.GetUserByEmail(userRequest.EmailAddress)).Returns(user);

            Action act = () => _usersUseCase.ExecutePost(userRequest);

            act.Should().Throw<PostUserException>().WithMessage($"User with email {userRequest.EmailAddress} already exists on the system");
        }

        [Test]
        public void ExecuteGetAllCallsUsersGateway()
        {
            _usersUseCase.ExecuteGetAll();
            _mockUsersGateway.Verify(x => x.GetUsers(), Times.Once);
        }

        [Test]
        public void ExecuteGetAllReturnsListOfUsers()
        {
            var userList = _fixture.CreateMany<dbUser>().AsEnumerable();
            _mockUsersGateway.Setup(x => x.GetUsers()).Returns(userList);

            var expectedResponse = userList.Select(x => x.ToDomain().ToResponse()).ToList();

            var response = _usersUseCase.ExecuteGetAll();

            response.Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public void ExecuteGetAllReturnsEmptyListIfNoUsersFound()
        {
            var emptyUserList = Enumerable.Empty<dbUser>();
            _mockUsersGateway.Setup(x => x.GetUsers()).Returns(emptyUserList);

            var expectedResponse = new List<UserResponse>();

            var response = _usersUseCase.ExecuteGetAll();

            response.Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public void ExecuteGetByEmailCallsUserGateway()
        {
            var getUserRequest = TestHelpers.CreateGetUserRequest();
            var user = TestHelpers.CreateUser(email: getUserRequest.EmailAddress);
            _mockUsersGateway.Setup(x => x.GetUserByEmail(getUserRequest.EmailAddress)).Returns(user);

            var response = _usersUseCase.ExecuteGetByEmail(getUserRequest);

            _mockUsersGateway.Verify(x => x.GetUserByEmail(getUserRequest.EmailAddress));
            _mockUsersGateway.Verify(x => x.GetUserByEmail(It.Is<string>(u => u == getUserRequest.EmailAddress)), Times.Once());
        }

        [Test]
        public void ExecuteGetByEmailReturnsAUser()
        {
            var getUserRequest = TestHelpers.CreateGetUserRequest();
            var user = TestHelpers.CreateUser(email: getUserRequest.EmailAddress);
            _mockUsersGateway.Setup(x => x.GetUserByEmail(getUserRequest.EmailAddress)).Returns(user);

            var response = _usersUseCase.ExecuteGetByEmail(getUserRequest);

            response.Should().BeEquivalentTo(user.ToDomain().ToResponse());
        }

        [Test]
        public void ExecuteGetByEmailReturnsNullIfUserNotFound()
        {
            var getUserRequest = TestHelpers.CreateGetUserRequest();

            var response = _usersUseCase.ExecuteGetByEmail(getUserRequest);

            response.Should().BeNull();
        }
    }

}
