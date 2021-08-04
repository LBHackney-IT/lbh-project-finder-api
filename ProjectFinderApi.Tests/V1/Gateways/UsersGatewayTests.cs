using FluentAssertions;
using NUnit.Framework;
using ProjectFinderApi.Tests.V1.Helpers;
using ProjectFinderApi.V1.Gateways;

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
    }
}
