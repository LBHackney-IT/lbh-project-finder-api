using AutoFixture;
using ProjectFinderApi.V1.Domain;
using ProjectFinderApi.V1.Factories;
using dbUser = ProjectFinderApi.V1.Infrastructure;
using FluentAssertions;
using NUnit.Framework;
using Bogus;
using ProjectFinderApi.Tests.V1.Helpers;

namespace ProjectFinderApi.Tests.V1.Factories
{
    [TestFixture]
    public class EntityFactoryTest
    {
        private Faker _faker;
        private Fixture _fixture;

        public void SetUp()
        {
            _faker = new Faker();
            _fixture = new Fixture();
        }

        [Test]
        public void CanMapUserFromInfrastructureToDomain()
        {
            var dbUser = TestHelpers.CreateUser();

            var expectedResponse = new User()
            {
                Id = dbUser.Id,
                FirstName = dbUser.FirstName,
                LastName = dbUser.LastName,
                Email = dbUser.Email,
                Role = dbUser.Role,
            };

            dbUser.ToDomain().Should().BeEquivalentTo(expectedResponse);
        }

    }
}
