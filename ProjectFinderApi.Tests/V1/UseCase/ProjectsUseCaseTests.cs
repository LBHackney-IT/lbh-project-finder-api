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
    public class ProjectsUseCaseTests
    {
        private Mock<IProjectsGateway> _mockProjectsGateway;

        private IProjectsUseCase _projectsUseCase;

        [SetUp]
        public void SetUp()
        {
            _mockProjectsGateway = new Mock<IProjectsGateway>();
            _projectsUseCase = new ProjectsUseCase(_mockProjectsGateway.Object);
        }

        [Test]
        public void ExecutePostCallsProjectsGateway()
        {
            var projectRequest = TestHelpers.CreateProjectRequest();
            var project = TestHelpers.CreateProject(
                projectName: projectRequest.ProjectName,
                description: projectRequest.Description,
                projectContact: projectRequest.ProjectContact,
                phase: projectRequest.Phase,
                size: projectRequest.Size,
                category: projectRequest.Category,
                priority: projectRequest.Priority,
                productUsers: projectRequest.ProductUsers,
                dependencies: projectRequest.Dependencies);

            _mockProjectsGateway.Setup(x => x.CreateProject(projectRequest)).Returns(project);

            var response = _projectsUseCase.ExecutePost(projectRequest);

            _mockProjectsGateway.Verify(x => x.CreateProject(projectRequest));
            _mockProjectsGateway.Verify(x => x.CreateProject(It.Is<CreateProjectRequest>(u => u == projectRequest)), Times.Once());

        }

        [Test]
        public void ExecutePostReturnsCreatedProjects()
        {
            var projectRequest = TestHelpers.CreateProjectRequest();
            var project = TestHelpers.CreateProject(
                projectName: projectRequest.ProjectName,
                description: projectRequest.Description,
                projectContact: projectRequest.ProjectContact,
                phase: projectRequest.Phase,
                size: projectRequest.Size,
                category: projectRequest.Category,
                priority: projectRequest.Priority,
                productUsers: projectRequest.ProductUsers,
                dependencies: projectRequest.Dependencies);

            _mockProjectsGateway.Setup(x => x.CreateProject(projectRequest)).Returns(project);

            var response = _projectsUseCase.ExecutePost(projectRequest);

            response.Should().BeEquivalentTo(project.ToDomain().ToResponse());

        }
    }

}
