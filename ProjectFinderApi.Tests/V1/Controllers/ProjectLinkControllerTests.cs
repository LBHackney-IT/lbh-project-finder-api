using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProjectFinderApi.Tests.V1.Helpers;
using ProjectFinderApi.V1.Boundary.Response;
using ProjectFinderApi.V1.Controllers;
using ProjectFinderApi.V1.Exceptions;
using ProjectFinderApi.V1.UseCase.Interfaces;

namespace ProjectFinderApi.Tests.V1.Controllers
{
    [TestFixture]
    public class ProjectLinkControllerTests
    {
        private ProjectLinkController _projectLinkController;

        private Mock<IProjectLinksUseCase> _projectLinksUseCase;

        [SetUp]
        public void SetUp()
        {
            _projectLinksUseCase = new Mock<IProjectLinksUseCase>();
            _projectLinkController = new ProjectLinkController(_projectLinksUseCase.Object);

        }

        [Test]
        public void CreateProjectLinkReturns204WhenALinkIsSuccessfullyAdded()
        {
            var request = TestHelpers.CreateProjectLinkRequest();
            _projectLinksUseCase.Setup(x => x.ExecutePost(request));

            var response = _projectLinkController.CreateProjectLink(request) as NoContentResult;

            response.StatusCode.Should().Be(204);
        }

        [Test]
        public void CreateProjectLinkReturns400WhenValidationResultIsNotValid()
        {
            var request = TestHelpers.CreateProjectLinkRequest(linkTitle: "");

            var response = _projectLinkController.CreateProjectLink(request) as BadRequestObjectResult;

            response.StatusCode.Should().Be(400);
            response.Value.Should().Be("A link title must be provided");
        }

        [Test]
        public void CreateProjectLinkReturns422WhenPostProjectLinkExceptionIsThrown()
        {
            var request = TestHelpers.CreateProjectLinkRequest();
            _projectLinksUseCase.Setup(x => x.ExecutePost(request)).Throws(new PostProjectLinkException($"The project with the id of {request.ProjectId} could not be found"));

            var response = _projectLinkController.CreateProjectLink(request) as ObjectResult;

            response.StatusCode.Should().Be(422);
            response.Value.Should().Be($"The project with the id of {request.ProjectId} could not be found");
        }

        [Test]
        public void GetProjectLinksByProjectIdReturns200WhenLinksFound()
        {
            var projectId = 1;
            var linksResponse = new List<ProjectLinkResponse> { TestHelpers.CreateProjectLinkResponse() };
            _projectLinksUseCase.Setup(x => x.ExecuteGet(projectId)).Returns(linksResponse);

            var response = _projectLinkController.GetProjectLinksByProjectId(projectId) as ObjectResult;

            response.StatusCode.Should().Be(200);
            response.Value.Should().BeEquivalentTo(linksResponse);
        }

        [Test]
        public void GetProjectLinksByProjectIdReturns404WhenNoLinksFound()
        {
            var projectId = 1;
            var links = new List<ProjectLinkResponse>();
            _projectLinksUseCase.Setup(x => x.ExecuteGet(projectId)).Returns(links);

            var response = _projectLinkController.GetProjectLinksByProjectId(projectId) as NotFoundObjectResult;

            response.StatusCode.Should().Be(404);
            response.Value.Should().Be("No links found for that project ID");
        }

        [Test]
        public void DeleteProjectLinkReturns200WhenLinkIsSuccessfullyDeleted()
        {
            _projectLinksUseCase.Setup(x => x.ExecuteDelete(1));
            var response = _projectLinkController.DeleteProjectLink(1) as StatusCodeResult;

            response.StatusCode.Should().Be(200);

        }

        [Test]
        public void DeleteProjectLinkReturns422WhenGetProjectLinkExceptionIsThrown()
        {
            var nonExisitentId = 1;
            _projectLinksUseCase.Setup(x => x.ExecuteDelete(nonExisitentId)).Throws(new GetProjectLinksException($"Project link with ID: {nonExisitentId} not found"));
            var response = _projectLinkController.DeleteProjectLink(nonExisitentId) as ObjectResult;

            response?.StatusCode.Should().Be(422);
            response?.Value.Should().Be($"Project link with ID: {nonExisitentId} not found");
        }

    }
}
