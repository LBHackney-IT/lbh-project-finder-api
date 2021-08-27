using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using ProjectFinderApi.Tests.V1.Helpers;
using ProjectFinderApi.V1.Boundary.Request;
using ProjectFinderApi.V1.Boundary.Response;
using ProjectFinderApi.V1.Exceptions;
using ProjectFinderApi.V1.Factories;
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

        //   private readonly Fixture _fixture = new Fixture();

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
            _mockProjectMembersGateway.Setup(x => x.CreateProjectMember(request));

            _projectMembersUseCase.ExecutePost(request);

            _mockProjectMembersGateway.Verify(x => x.CreateProjectMember(request));
            _mockProjectMembersGateway.Verify(x => x.CreateProjectMember(It.Is<CreateProjectMemberRequest>(m => m == request)), Times.Once());
        }
    }
}
