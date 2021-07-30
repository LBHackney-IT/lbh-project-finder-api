using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProjectFinderApi.Tests.V1.Helpers;
using ProjectFinderApi.V1.Controllers;
using ProjectFinderApi.V1.Exceptions;
using ProjectFinderApi.V1.UseCase.Interfaces;

namespace ProjectFinderApi.Tests.V1.Controllers
{
    [TestFixture]
    public class UserControllerTests
    {
        private UserController _userController;

        private Mock<IUsersUseCase> _usersUseCase;

        [SetUp]
        public void SetUp()
        {
            _usersUseCase = new Mock<IUsersUseCase>();
            _userController = new UserController(_usersUseCase.Object);
        }

        [Test]
        public void CreateUserReturns201StatusAndUserWhenSuccessful()
        {
            //arrange
            var userRequest = TestHelpers.CreateUserRequest();
            var user = TestHelpers.CreateUserResponse(firstName: userRequest.FirstName, lastName: userRequest.LastName, email: userRequest.EmailAddress, role: userRequest.Role);
            _usersUseCase.Setup(x => x.ExecutePost(userRequest)).Returns(user);

            //act
            var response = _userController.CreateUser(userRequest) as ObjectResult;

            //assert
            response?.StatusCode.Should().Be(201);
            response?.Value.Should().BeEquivalentTo(user);
        }

        [Test]
        public void CreateUserReturns400WhenValidationResultIsNotValid()
        {
            var userRequest = TestHelpers.CreateUserRequest(firstName: "");

            var response = _userController.CreateUser(userRequest) as BadRequestObjectResult;

            response?.StatusCode.Should().Be(400);
            response.Value.Should().Be("First name must be provided");
        }

        [Test]
        public void CreateUserReturns422WhenPostWorkerExceptionThrown()
        {
            //arrange
            const string errorMessage = "Failed to create user";
            var userRequest = TestHelpers.CreateUserRequest();
            _usersUseCase.Setup(x => x.ExecutePost(userRequest)).Throws(new PostUserException(errorMessage));

            //act
            var response = _userController.CreateUser(userRequest) as ObjectResult;

            //assert
            response?.StatusCode.Should().Be(422);
            response?.Value.Should().BeEquivalentTo(errorMessage);
        }


    }
}
