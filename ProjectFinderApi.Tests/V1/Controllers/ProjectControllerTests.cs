using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProjectFinderApi.Tests.V1.Helpers;
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



    }
}
