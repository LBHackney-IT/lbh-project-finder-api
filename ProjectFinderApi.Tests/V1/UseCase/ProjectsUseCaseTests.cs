using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using ProjectFinderApi.Tests.V1.Helpers;
using ProjectFinderApi.V1.Boundary.Request;
using ProjectFinderApi.V1.Boundary.Response;
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

        private readonly Fixture _fixture = new Fixture();

        private const int MinimumLimit = 10;
        private const int MaximumLimit = 100;

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

        [Test]
        public void ExecuteGetCallsProjectsGateway()
        {
            var projectRequest = TestHelpers.GetProjectRequest();

            var response = _projectsUseCase.ExecuteGet(projectRequest);

            _mockProjectsGateway.Verify(x => x.GetProjectById(projectRequest));
            _mockProjectsGateway.Verify(x => x.GetProjectById(It.Is<GetProjectRequest>(u => u == projectRequest)), Times.Once());
        }

        [Test]
        public void ExecuteGetReturnsAProject()
        {
            var projectRequest = TestHelpers.GetProjectRequest();
            var project = TestHelpers.CreateProject(projectRequest.Id);

            _mockProjectsGateway.Setup(x => x.GetProjectById(projectRequest)).Returns(project);

            var response = _projectsUseCase.ExecuteGet(projectRequest);

            response.Should().BeEquivalentTo(project.ToDomain().ToResponse());
        }

        [Test]
        public void ExecuteGetReturnsNullIfNoProjectIsFound()
        {
            var projectRequest = TestHelpers.GetProjectRequest();

            var response = _projectsUseCase.ExecuteGet(projectRequest);

            response.Should().BeNull();
        }

        [Test]
        public void ExecutePatchCallsProjectsGateway()
        {
            var projectRequest = TestHelpers.UpdateProjectRequest();
            _mockProjectsGateway.Setup(x => x.UpdateProject(projectRequest));

            _projectsUseCase.ExecutePatch(projectRequest);

            _mockProjectsGateway.Verify(x => x.UpdateProject(projectRequest));
            _mockProjectsGateway.Verify(x => x.UpdateProject(It.Is<UpdateProjectRequest>(u => u == projectRequest)), Times.Once());
        }

        [Test]
        public void ExecuteDeleteCallsProjectsGateway()
        {
            var projectId = 1;
            _mockProjectsGateway.Setup(x => x.DeleteProject(projectId));

            _projectsUseCase.ExecuteDelete(projectId);

            _mockProjectsGateway.Verify(x => x.DeleteProject(projectId));

            _mockProjectsGateway.Verify(x => x.DeleteProject(It.Is<int>(u => u == projectId)), Times.Once());

        }

        [Test]
        public void ExecuteGetAllByQueryReturnsAnEmptyListIfNoResultsFound()
        {
            var searchQuery = new ProjectQueryParams { ProjectName = "test" };
            var emptyResponse = new ProjectListResponse { Projects = new List<ProjectResponse>() };
            var emptyList = new List<ProjectResponse>();
            _mockProjectsGateway.Setup(x => x.GetProjectsByQuery(0, 20, searchQuery.ProjectName, null, null)).Returns(emptyList);

            var response = _projectsUseCase.ExecuteGetAllByQuery(searchQuery, 0, 20);

            response.Should().BeEquivalentTo(emptyResponse);
        }

        [Test]
        public void ExecuteGetAllByQueryReturnsAListOfProjects()
        {
            var searchQuery = new ProjectQueryParams { ProjectName = "test" };
            var projectList = new List<ProjectResponse> { TestHelpers.CreateProjectResponse(projectName: searchQuery.ProjectName) };
            _mockProjectsGateway.Setup(x => x.GetProjectsByQuery(0, 20, searchQuery.ProjectName, null, null)).Returns(projectList);

            var response = _projectsUseCase.ExecuteGetAllByQuery(searchQuery, 0, 20);

            response.Should().NotBeNull();
            response.Projects.Should().BeEquivalentTo(projectList);
        }

        [Test]
        public void ExecuteGetAllByQueryCallsGatewayWithLimitAndCursor()
        {
            var cursor = 0;
            var limit = 20;
            var searchQuery = new ProjectQueryParams { ProjectName = "test" };
            var projectList = new List<ProjectResponse> { TestHelpers.CreateProjectResponse(projectName: searchQuery.ProjectName) };
            _mockProjectsGateway.Setup(x => x.GetProjectsByQuery(cursor, limit, searchQuery.ProjectName, null, null)).Returns(projectList);

            _projectsUseCase.ExecuteGetAllByQuery(searchQuery, cursor, limit);

            _mockProjectsGateway.Verify(x => x.GetProjectsByQuery(It.Is<int>(x => x == cursor), It.Is<int>(x => x == limit), It.Is<string>(x => x == searchQuery.ProjectName), It.Is<string>(x => x == null), It.Is<string>(x => x == null)), Times.Once());
        }

        [Test]
        public void ExecuteGetAllByQueryReturnsTheNextCursor()
        {
            var searchQuery = new ProjectQueryParams();
            var projects = _fixture.CreateMany<ProjectResponse>(20).OrderBy(x => x.Id).ToList();
            _mockProjectsGateway.Setup(x => x.GetProjectsByQuery(0, 20, null, null, null)).Returns(projects);

            var expectedNextCursor = projects.Last().Id.ToString();

            var response = _projectsUseCase.ExecuteGetAllByQuery(searchQuery, 0, 20);

            response.NextCursor.Should().Be(expectedNextCursor);

        }

        [Test]
        public void WhenAtEndOfProjectListTheNextCursorShouldBeNull()
        {
            var searchQuery = new ProjectQueryParams();
            var projects = _fixture.CreateMany<ProjectResponse>(15).OrderBy(x => x.Id).ToList();
            _mockProjectsGateway.Setup(x => x.GetProjectsByQuery(0, 20, null, null, null)).Returns(projects);


            var response = _projectsUseCase.ExecuteGetAllByQuery(searchQuery, 0, 20);

            response.NextCursor.Should().BeNull();

        }

        [Test]
        public void IfExecuteGetAllByQueryLimitIsLessThanTheMinimumTheLimitWillBeTheMinimum()
        {
            var searchQuery = new ProjectQueryParams();
            var projects = _fixture.CreateMany<ProjectResponse>(10).OrderBy(x => x.Id).ToList();
            _mockProjectsGateway.Setup(x => x.GetProjectsByQuery(0, MinimumLimit, null, null, null)).Returns(projects);

            _projectsUseCase.ExecuteGetAllByQuery(searchQuery, 0, 5);

            _mockProjectsGateway.Verify(x => x.GetProjectsByQuery(0, MinimumLimit, null, null, null));
        }

        [Test]
        public void IfExecuteGetAllByQueryLimitIsOnTheMinimumBoundaryTheLimitWillBeTheMinimum()
        {

            var searchQuery = new ProjectQueryParams();
            var projects = _fixture.CreateMany<ProjectResponse>(10).OrderBy(x => x.Id).ToList();
            _mockProjectsGateway.Setup(x => x.GetProjectsByQuery(0, MinimumLimit, null, null, null)).Returns(projects);

            _projectsUseCase.ExecuteGetAllByQuery(searchQuery, 0, 10);

            _mockProjectsGateway.Verify(x => x.GetProjectsByQuery(0, MinimumLimit, null, null, null));
        }

        [Test]
        public void IfExecuteGetAllByQueryLimitIsMoreThanTheMaximumTheLimitWillBeTheMaximum()
        {
            var searchQuery = new ProjectQueryParams();
            var projects = _fixture.CreateMany<ProjectResponse>(10).OrderBy(x => x.Id).ToList();
            _mockProjectsGateway.Setup(x => x.GetProjectsByQuery(0, MaximumLimit, null, null, null)).Returns(projects);

            _projectsUseCase.ExecuteGetAllByQuery(searchQuery, 0, 200);

            _mockProjectsGateway.Verify(x => x.GetProjectsByQuery(0, MaximumLimit, null, null, null));
        }

        [Test]
        public void IfExecuteGetAllByQueryLimitIsOnTheMaximumBoundaryTheLimitWillBeTheMaximum()
        {

            var searchQuery = new ProjectQueryParams();
            var projects = _fixture.CreateMany<ProjectResponse>(10).OrderBy(x => x.Id).ToList();
            _mockProjectsGateway.Setup(x => x.GetProjectsByQuery(0, MaximumLimit, null, null, null)).Returns(projects);

            _projectsUseCase.ExecuteGetAllByQuery(searchQuery, 0, 100);

            _mockProjectsGateway.Verify(x => x.GetProjectsByQuery(0, MaximumLimit, null, null, null));
        }
    }
}
