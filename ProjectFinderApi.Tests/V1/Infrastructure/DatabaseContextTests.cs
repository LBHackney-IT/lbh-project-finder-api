using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using ProjectFinderApi.Tests.V1.Helpers;

namespace ProjectFinderApi.Tests.V1.Infrastructure
{
    //TODO: Remove this file if Postgres is not being used
    [TestFixture]
    public class DatabaseContextTest : DatabaseTests
    {
        [Test]
        public void CanCreateADatabaseRecordForAUser()
        {
            var user = TestHelpers.CreateUser();

            DatabaseContext.Add(user);
            DatabaseContext.SaveChanges();

            var result = DatabaseContext.Users.ToList().FirstOrDefault();

            result.Should().BeEquivalentTo(user);
        }
    }
}
