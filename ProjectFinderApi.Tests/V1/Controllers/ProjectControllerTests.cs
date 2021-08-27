using AutoFixture;
using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProjectFinderApi.Tests.V1.Helpers;
using ProjectFinderApi.V1.Boundary.Request;
using ProjectFinderApi.V1.Boundary.Response;
using ProjectFinderApi.V1.Controllers;
using ProjectFinderApi.V1.Exceptions;
using ProjectFinderApi.V1.UseCase.Interfaces;

namespace ProjectFinderApi.Tests.V1.Controllers
{
    [TestFixture]
    public class ProjectControllerTests
    {
        private ProjectController _projectController;

        private Mock<IProjectsUseCase> _projectsUseCase;

        private readonly Faker _faker = new Faker();

        private readonly Fixture _fixture = new Fixture();


        [SetUp]
        public void SetUp()
        {
            _projectsUseCase = new Mock<IProjectsUseCase>();
            _projectController = new ProjectController(_projectsUseCase.Object);
        }

        [Test]
        public void CreateProjectReturns201StatusAndProjectWhenSuccessful()
        {
            var projectRequest = TestHelpers.CreateProjectRequest(projectContact: _faker.Random.String2(100), category: _faker.Random.String2(70), priority: _faker.Random.String2(50), productUsers: _faker.Random.String2(500), dependencies: _faker.Random.String2(300));
            var project = TestHelpers.CreateProjectResponse(
                projectName: projectRequest.ProjectName,
                description: projectRequest.Description,
                projectContact: projectRequest.ProjectContact,
                phase: projectRequest.Phase,
                size: projectRequest.Size,
                category: projectRequest.Category,
                priority: projectRequest.Priority,
                productUsers: projectRequest.ProductUsers,
                dependencies: projectRequest.Dependencies);

            _projectsUseCase.Setup(x => x.ExecutePost(projectRequest)).Returns(project);

            var response = _projectController.CreateProject(projectRequest) as ObjectResult;

            response?.StatusCode.Should().Be(201);
            response?.Value.Should().BeEquivalentTo(project);
        }

        [Test]
        public void CreateProjectWithNullValuesReturns201StatusAndProjectWhenSuccessful()
        {
            var projectRequest = TestHelpers.CreateProjectRequest();
            var project = TestHelpers.CreateProjectResponse(
                projectName: projectRequest.ProjectName,
                description: projectRequest.Description,
                projectContact: projectRequest.ProjectContact,
                phase: projectRequest.Phase,
                size: projectRequest.Size,
                category: projectRequest.Category,
                priority: projectRequest.Priority,
                productUsers: projectRequest.ProductUsers,
                dependencies: projectRequest.Dependencies);

            _projectsUseCase.Setup(x => x.ExecutePost(projectRequest)).Returns(project);

            var response = _projectController.CreateProject(projectRequest) as ObjectResult;

            project.ProjectContact.Should().BeNull();
            response?.StatusCode.Should().Be(201);
            response?.Value.Should().BeEquivalentTo(project);
        }

        [Test]
        public void CreateProjectReturns400WhenValidationResultIsNotValid()
        {
            var projectRequest = TestHelpers.CreateProjectRequest(projectName: "");

            var response = _projectController.CreateProject(projectRequest) as BadRequestObjectResult;

            response?.StatusCode.Should().Be(400);
            response?.Value.Should().Be("Project name must be provided");
        }

        [Test]
        public void CreateProjectReturns422WhenPostProjectExceptionThrown()
        {
            //arrange
            const string errorMessage = "Failed to create project";
            var projectRequest = TestHelpers.CreateProjectRequest();
            _projectsUseCase.Setup(x => x.ExecutePost(projectRequest)).Throws(new PostProjectException(errorMessage));

            //act
            var response = _projectController.CreateProject(projectRequest) as ObjectResult;

            //assert
            response?.StatusCode.Should().Be(422);
            response?.Value.Should().BeEquivalentTo(errorMessage);
        }

        [Test]
        public void GetProjectReturns200WhenProjectIsFound()
        {
            var projectRequest = TestHelpers.GetProjectRequest();
            var project = TestHelpers.CreateProjectResponse(projectRequest.Id);
            _projectsUseCase.Setup(x => x.ExecuteGet(projectRequest)).Returns(project);

            var response = _projectController.GetProject(projectRequest) as ObjectResult;

            response?.StatusCode.Should().Be(200);
            response?.Value.Should().BeEquivalentTo(project);
        }

        [Test]
        public void GetProjectReturns400WhenProjectIsNotFound()
        {
            var projectRequest = TestHelpers.GetProjectRequest();

            var response = _projectController.GetProject(projectRequest) as NotFoundObjectResult;

            response?.StatusCode.Should().Be(404);
            response?.Value.Should().Be("No project found with that ID");
        }

        [Test]
        public void UpdateProjectReturns201WhenProjectIsSuccessfullyUpdated()
        {
            var projectRequest = TestHelpers.UpdateProjectRequest();
            _projectsUseCase.Setup(x => x.ExecutePatch(projectRequest));

            var response = _projectController.UpdateProject(projectRequest) as NoContentResult;

            response?.StatusCode.Should().Be(204);
        }

        [Test]
        public void UpdateProjectReturns400WhenValidationResultIsNotValid()
        {
            var projectRequest = TestHelpers.UpdateProjectRequest(description: "");

            var response = _projectController.UpdateProject(projectRequest) as BadRequestObjectResult;

            response?.StatusCode.Should().Be(400);
        }

        [Test]
        public void UpdateProjectReturns422WhenUpdateProjectExceptiontIsThrown()
        {
            var projectRequest = TestHelpers.UpdateProjectRequest();
            _projectsUseCase.Setup(x => x.ExecutePatch(projectRequest)).Throws(new PatchProjectException($"Project with ID {projectRequest.Id} not found"));

            var response = _projectController.UpdateProject(projectRequest) as ObjectResult;

            response?.StatusCode.Should().Be(422);
            response?.Value.Should().Be($"Project with ID {projectRequest.Id} not found");
        }

        [Test]
        public void DeleteProjectReturns200WhenProjectIsSucessfullyDeleted()
        {
            var response = _projectController.DeleteProject(1) as ObjectResult;

            response?.StatusCode.Should().Be(200);
        }

        [Test]
        public void DeleteProjectReturns400WhenDeleteProjectExceptionIsThrown()
        {
            var nonExisitentProjectId = 1;
            _projectsUseCase.Setup(x => x.ExecuteDelete(nonExisitentProjectId)).Throws(new DeleteProjectException($"Project with ID {nonExisitentProjectId} not found"));

            var response = _projectController.DeleteProject(nonExisitentProjectId) as BadRequestObjectResult;

            response?.StatusCode.Should().Be(400);
            response?.Value.Should().Be($"Project with ID {nonExisitentProjectId} not found");
        }

        [Test]
        public void GetProjectBySearchQueryReturns200WhenSuccessful()
        {
            var projectResponseList = _fixture.Create<ProjectListResponse>();
            var projectSearchQuery = new ProjectQueryParams();
            _projectsUseCase.Setup(x => x.ExecuteGetAllByQuery(projectSearchQuery, 0, 20)).Returns(projectResponseList);

            var response = _projectController.GetProjectsBySearchQuery(projectSearchQuery) as OkObjectResult;

            response?.StatusCode.Should().Be(200);
            response?.Value.Should().Be(projectResponseList);
        }

        [Test]
        public void GetProjectBySearchQueryReturns404WhenNoProjectsFound()
        {

            var projectSearchQuery = new ProjectQueryParams();
            _projectsUseCase.Setup(x => x.ExecuteGetAllByQuery(projectSearchQuery, 0, 20)).Throws(new GetProjectsException("No projects found"));

            var response = _projectController.GetProjectsBySearchQuery(projectSearchQuery) as NotFoundObjectResult;

            response?.StatusCode.Should().Be(404);
            response?.Value.Should().Be("No projects found");
        }
    }
}
