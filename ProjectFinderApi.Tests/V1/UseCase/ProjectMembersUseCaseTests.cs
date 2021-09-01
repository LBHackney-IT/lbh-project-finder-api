using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using ProjectFinderApi.Tests.V1.Helpers;
using ProjectFinderApi.V1.Boundary.Request;
using ProjectFinderApi.V1.Boundary.Response;
using ProjectFinderApi.V1.Exceptions;
using ProjectFinderApi.V1.Gateways.Interfaces;
using ProjectFinderApi.V1.UseCase;
using ProjectFinderApi.V1.UseCase.Interfaces;

namespace ProjectFinderApi.Tests.V1.UseCase
{
    [TestFixture]
    public class ProjectMembersUseCaseTests
    {
        private Mock<IProjectMembersGateway> _mockProjectMembersGateway;

        private IProjectMembersUseCase _projectMembersUseCase;

        [SetUp]
        public void SetUp()
        {
            _mockProjectMembersGateway = new Mock<IProjectMembersGateway>();
            _projectMembersUseCase = new ProjectMembersUseCase(_mockProjectMembersGateway.Object);
        }

        [Test]
        public void ExecutePostCallsProjectMembersGateway()
        {
            var request = TestHelpers.CreateProjectMemberRequest();
            var members = new List<ProjectMemberResponse>();
            _mockProjectMembersGateway.Setup(x => x.GetProjectMembersByUserId(request.UserId)).Returns(members);
            _mockProjectMembersGateway.Setup(x => x.CreateProjectMember(request));

            _projectMembersUseCase.ExecutePost(request);

            _mockProjectMembersGateway.Verify(x => x.CreateProjectMember(request));
            _mockProjectMembersGateway.Verify(x => x.CreateProjectMember(It.Is<CreateProjectMemberRequest>(m => m == request)), Times.Once());
        }

        [Test]
        public void ExecutePostThrowsAnExceptionIfUserIsAlreadyAssigned()
        {
            var request = TestHelpers.CreateProjectMemberRequest();
            var members = new List<ProjectMemberResponse>() { TestHelpers.CreateProjectMemberResponse(projectId: request.ProjectId) };
            _mockProjectMembersGateway.Setup(x => x.GetProjectMembersByUserId(request.UserId)).Returns(members);

            Action act = () => _projectMembersUseCase.ExecutePost(request);

            act.Should().Throw<PostProjectMemberException>().WithMessage($"The user with the id of {request.UserId} is already assigned to the project");
        }

        [Test]
        public void ExecuteGetByProjectIdCallsProjectMembersGateway()
        {
            var request = 1;
            _mockProjectMembersGateway.Setup(x => x.GetProjectMembersByProjectId(request));

            _projectMembersUseCase.ExecuteGetByProjectId(request);

            _mockProjectMembersGateway.Verify(x => x.GetProjectMembersByProjectId(request));
            _mockProjectMembersGateway.Verify(x => x.GetProjectMembersByProjectId(It.Is<int>(m => m == request)), Times.Once());
        }

        [Test]
        public void ExecuteGetByProjectIdReturnsAProjectMemberResponseList()
        {
            var request = 1;
            var members = new List<ProjectMemberResponse>() { TestHelpers.CreateProjectMemberResponse(projectId: request) };
            _mockProjectMembersGateway.Setup(x => x.GetProjectMembersByProjectId(request)).Returns(members);

            var response = _projectMembersUseCase.ExecuteGetByProjectId(request);

            Assert.IsInstanceOf<List<ProjectMemberResponse>>(response);
            response.Should().BeEquivalentTo(members);


        }

        [Test]
        public void ExecuteGetByProjectIdReturnsAEmptyProjectMemberResponseListIfNoMembersFound()
        {
            var request = 1;
            var members = new List<ProjectMemberResponse>();
            _mockProjectMembersGateway.Setup(x => x.GetProjectMembersByProjectId(request)).Returns(members);

            var response = _projectMembersUseCase.ExecuteGetByProjectId(request);

            Assert.IsInstanceOf<List<ProjectMemberResponse>>(response);
            response.Should().BeEmpty();
        }

        [Test]
        public void ExecuteGetByUserIdCallsProjectMembersGateway()
        {
            var request = 1;
            _mockProjectMembersGateway.Setup(x => x.GetProjectMembersByUserId(request));

            _projectMembersUseCase.ExecuteGetByUserId(request);

            _mockProjectMembersGateway.Verify(x => x.GetProjectMembersByUserId(request));
            _mockProjectMembersGateway.Verify(x => x.GetProjectMembersByUserId(It.Is<int>(m => m == request)), Times.Once());
        }

        [Test]
        public void ExecuteGetByUserIdReturnsAProjectMemberResponseList()
        {
            var request = 1;
            var members = new List<ProjectMemberResponse>() { TestHelpers.CreateProjectMemberResponse() };
            _mockProjectMembersGateway.Setup(x => x.GetProjectMembersByUserId(request)).Returns(members);

            var response = _projectMembersUseCase.ExecuteGetByUserId(request);

            Assert.IsInstanceOf<List<ProjectMemberResponse>>(response);
            response.Should().BeEquivalentTo(members);
        }

        [Test]
        public void ExecuteGetByUserIdReturnsAEmptyProjectMemberResponseListIfNoMembersFound()
        {
            var request = 1;
            var members = new List<ProjectMemberResponse>();
            _mockProjectMembersGateway.Setup(x => x.GetProjectMembersByUserId(request)).Returns(members);

            var response = _projectMembersUseCase.ExecuteGetByUserId(request);

            Assert.IsInstanceOf<List<ProjectMemberResponse>>(response);
            response.Should().BeEmpty();
        }

        [Test]
        public void ExecuteDeleteCallsProjectMembersGateway()
        {
            var memberId = 1;
            _mockProjectMembersGateway.Setup(x => x.DeleteProjectMember(memberId));

            _projectMembersUseCase.ExecuteDelete(memberId);

            _mockProjectMembersGateway.Verify(x => x.DeleteProjectMember(memberId));
            _mockProjectMembersGateway.Verify(x => x.DeleteProjectMember(It.Is<int>(m => m == memberId)), Times.Once());
        }

    }
}
