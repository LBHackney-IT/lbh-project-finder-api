using System.Collections.Generic;
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
    public class ProjectMemberControllerTests
    {
        private ProjectMemberController _projectMemberController;

        private Mock<IProjectMembersUseCase> _projectMembersUseCase;

        [SetUp]
        public void SetUp()
        {
            _projectMembersUseCase = new Mock<IProjectMembersUseCase>();
            _projectMemberController = new ProjectMemberController(_projectMembersUseCase.Object);

        }

        [Test]
        public void CreateProjectMemberReturns204WhenProjectMemberIsSuccessfullyAdded()
        {
            var request = TestHelpers.CreateProjectMemberRequest();
            _projectMembersUseCase.Setup(x => x.ExecutePost(request));

            var response = _projectMemberController.CreateProjectMember(request) as NoContentResult;

            response?.StatusCode.Should().Be(204);
        }

        [Test]
        public void CreateProjectMemberReturns400WhenValidationResultIsNotValid()
        {
            var request = TestHelpers.CreateProjectMemberRequest(projectRole: "");

            var response = _projectMemberController.CreateProjectMember(request) as BadRequestObjectResult;

            response?.StatusCode.Should().Be(400);
        }

        [Test]
        public void GetProjectMembersByProjectIdReturns200WhenMembersFound()
        {
            var request = 1;
            var members = new List<ProjectMemberResponse>() { TestHelpers.CreateProjectMemberResponse() };
            _projectMembersUseCase.Setup(x => x.ExecuteGetByProjectId(request)).Returns(members);

            var response = _projectMemberController.GetProjectMembersByProjectId(request) as ObjectResult;

            response?.StatusCode.Should().Be(200);
            response?.Value.Should().BeEquivalentTo(members);
        }

        [Test]
        public void GetProjectMembersByProjectIdReturns404WhenMembersNotFound()
        {
            var members = new List<ProjectMemberResponse>();
            var request = 1;
            _projectMembersUseCase.Setup(x => x.ExecuteGetByProjectId(request)).Returns(members);

            var response = _projectMemberController.GetProjectMembersByProjectId(request) as NotFoundObjectResult;

            response.StatusCode.Should().Be(404);
            response.Value.Should().Be("No members found for that project ID");
        }
    }
}
