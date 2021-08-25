using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using ProjectFinderApi.Tests.V1.Helpers;
using ProjectFinderApi.V1.Gateways;
using ProjectFinderApi.V1.Infrastructure;

namespace ProjectFinderApi.Tests.V1.Gateways
{
    public class UsersGatewayTests : DatabaseTests
    {
        private UsersGateway _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _classUnderTest = new UsersGateway(DatabaseContext);
        }

        [Test]
        public void CreateUserInsertsUserIntoDatabase()
        {
            var createdUserRequest = TestHelpers.CreateUserRequest();

            var returnedUser = _classUnderTest.CreateUser(createdUserRequest);

            returnedUser.Should().NotBeNull();
            returnedUser.FirstName.Should().Be(createdUserRequest.FirstName);
            returnedUser.LastName.Should().Be(createdUserRequest.LastName);
            returnedUser.Email.Should().Be(createdUserRequest.EmailAddress);
            returnedUser.Role.Should().Be(createdUserRequest.Role);
        }
        [Test]
        public void GetUsersReturnsListOfUsers()
        {
            var firstUser = SaveUserToDatabase(GatewayHelpers.CreateUserDatabaseEntity());
            var secondUser = SaveUserToDatabase(GatewayHelpers.CreateUserDatabaseEntity(id: 2, email: "test2email@example.com", firstName: "test2-first-name", lastName: "test2-first-name"));

            var response = _classUnderTest.GetUsers().ToList();

            response.Count.Should().Be(2);
            response.Should().ContainEquivalentOf(firstUser);
            response.Should().ContainEquivalentOf(secondUser);
        }

        [Test]
        public void GetUsersReturnsEmptyListOfUsersIfNoUsersFound()
        {

            var response = _classUnderTest.GetUsers();

            response.Should().BeEmpty();
        }

        private User SaveUserToDatabase(User user)
        {
            DatabaseContext.Users.Add(user);
            DatabaseContext.SaveChanges();
            return user;
        }

        [Test]
        public void GetUserByEmailReturnsAUser()
        {
            var getUserRequest = TestHelpers.CreateGetUserRequest();
            var user = SaveUserToDatabase(GatewayHelpers.CreateUserDatabaseEntity(email: getUserRequest.EmailAddress));

            var response = _classUnderTest.GetUserByEmail(getUserRequest.EmailAddress);

            response.Should().NotBeNull();
            response.Email.Should().Be(getUserRequest.EmailAddress);
        }

        [Test]
        public void GetUserByEmailReturnsNullIfNoUserFound()
        {
            var getUserRequest = TestHelpers.CreateGetUserRequest();

            var response = _classUnderTest.GetUserByEmail(getUserRequest.EmailAddress);

            response.Should().BeNull();
        }

    }
}
