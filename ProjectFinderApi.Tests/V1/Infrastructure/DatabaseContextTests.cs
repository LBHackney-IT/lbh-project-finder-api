using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using ProjectFinderApi.Tests.V1.Helpers;

namespace ProjectFinderApi.Tests.V1.Infrastructure
{
    [TestFixture]
    public class DatabaseContextTest : DatabaseTests
    {
        [Test]
        public void CanCreateADatabaseRecordForAUser()
        {
            var user = TestHelpers.CreateUser();

            DatabaseContext.Add(user);
            DatabaseContext.SaveChanges();

            var result = DatabaseContext.Users.FirstOrDefault();

            result.Should().BeEquivalentTo(user);
        }

        [Test]
        public void CanCreateADatabaseRecordForAProject()
        {
            var project = TestHelpers.CreateProject();

            DatabaseContext.Add(project);
            DatabaseContext.SaveChanges();

            var result = DatabaseContext.Projects.FirstOrDefault();

            result.Should().BeEquivalentTo(project);
        }
    }
}
