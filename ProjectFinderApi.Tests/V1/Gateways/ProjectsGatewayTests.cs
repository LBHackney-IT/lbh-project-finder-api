using FluentAssertions;
using NUnit.Framework;
using ProjectFinderApi.Tests.V1.Helpers;
using ProjectFinderApi.V1.Gateways;

namespace ProjectFinderApi.Tests.V1.Gateways
{
    public class ProjectsGatewayTests : DatabaseTests
    {
        private ProjectsGateway _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _classUnderTest = new ProjectsGateway(DatabaseContext);
        }

        [Test]
        public void CreateProjectInsertsProjectIntoDatabase()
        {
            var createdProjectRequest = TestHelpers.CreateProjectRequest();

            var returnedProject = _classUnderTest.CreateProject(createdProjectRequest);

            returnedProject.Should().NotBeNull();
            returnedProject.ProjectName.Should().Be(createdProjectRequest.ProjectName);
            returnedProject.Description.Should().Be(createdProjectRequest.Description);
            returnedProject.ProjectContact.Should().BeNull();
            returnedProject.Phase.Should().Be(createdProjectRequest.Phase);
            returnedProject.Size.Should().Be(createdProjectRequest.Size);
            returnedProject.Category.Should().BeNull();
            returnedProject.Priority.Should().BeNull();
            returnedProject.ProductUsers.Should().BeNull();
            returnedProject.Dependencies.Should().BeNull();

        }
    }
}
