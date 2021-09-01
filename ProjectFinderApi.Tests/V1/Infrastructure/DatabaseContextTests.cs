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

        [Test]
        public void CanCreateADatabaseRecordForAProjectMember()
        {
            var user = TestHelpers.CreateUser();
            var project = TestHelpers.CreateProject();
            var member = TestHelpers.CreateProjectMember(userId: user.Id, projectId: project.Id);

            DatabaseContext.Add(user);
            DatabaseContext.Add(project);
            DatabaseContext.Add(member);
            DatabaseContext.SaveChanges();

            var result = DatabaseContext.ProjectMembers.FirstOrDefault();

            result.Should().BeEquivalentTo(member);
        }

        [Test]
        public void CanCreateADatabaseRecordForAProjectLink()
        {
            var project = TestHelpers.CreateProject();
            var link = TestHelpers.CreateProjectLink(projectId: project.Id);

            DatabaseContext.Add(project);
            DatabaseContext.Add(link);
            DatabaseContext.SaveChanges();

            var result = DatabaseContext.ProjectLinks.FirstOrDefault();

            result.Should().BeEquivalentTo(link);
        }
    }
}
