using FluentAssertions;
using Moq;
using NUnit.Framework;
using ProjectFinderApi.Tests.V1.Helpers;
using ProjectFinderApi.V1.Boundary.Request;
using ProjectFinderApi.V1.Factories;
using ProjectFinderApi.V1.Gateways.Interfaces;
using ProjectFinderApi.V1.UseCase;
using ProjectFinderApi.V1.UseCase.Interfaces;

namespace ProjectFinderApi.Tests.V1.UseCase
{
    [TestFixture]
    public class UsersUseCaseTests
    {
        private Mock<IUsersGateway> _mockUsersGateway;

        private IUsersUseCase _usersUseCase;

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
    }

}
