using System;
using System.Collections.Generic;
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
    public class ProjectLinksUseCaseTests
    {
        private Mock<IProjectLinksGateway> _mockProjectLinksGateway;

        private IProjectLinksUseCase _projectLinksUseCase;

        [SetUp]
        public void SetUp()
        {
            _mockProjectLinksGateway = new Mock<IProjectLinksGateway>();
            _projectLinksUseCase = new ProjectLinksUseCase(_mockProjectLinksGateway.Object);
        }

        [Test]
        public void ExecutePostCallsProjectLinksGateway()
        {
            var request = TestHelpers.CreateProjectLinkRequest();
            _mockProjectLinksGateway.Setup(x => x.CreateProjectLink(request));

            _projectLinksUseCase.ExecutePost(request);

            _mockProjectLinksGateway.Verify(x => x.CreateProjectLink(request));
            _mockProjectLinksGateway.Verify(x => x.CreateProjectLink(It.Is<CreateProjectLinkRequest>(l => l == request)), Times.Once());
        }

        [Test]
        public void ExecuteGetCallsProjectLinksGateway()
        {
            _mockProjectLinksGateway.Setup(x => x.GetProjectLinks(1));

            _projectLinksUseCase.ExecuteGet(1);

            _mockProjectLinksGateway.Verify(x => x.GetProjectLinks(1));
            _mockProjectLinksGateway.Verify(x => x.GetProjectLinks(It.Is<int>(l => l == 1)), Times.Once());
        }

        [Test]
        public void ExecuteGetReturnsAListOfProjectLinks()
        {
            var linksList = new List<ProjectLinkResponse>() { TestHelpers.CreateProjectLinkResponse() };
            _mockProjectLinksGateway.Setup(x => x.GetProjectLinks(1)).Returns(linksList);

            var response = _projectLinksUseCase.ExecuteGet(1);

            response.Should().BeEquivalentTo(linksList);
        }

        [Test]
        public void ExecuteGetReturnsAEmptyListIfNoProjectLinksFound()
        {
            var linksList = new List<ProjectLinkResponse>();
            _mockProjectLinksGateway.Setup(x => x.GetProjectLinks(1)).Returns(linksList);

            var response = _projectLinksUseCase.ExecuteGet(1);

            response.Should().BeEquivalentTo(linksList);
        }

        [Test]
        public void ExecuteDeleteCallsProjectLinksGateway()
        {
            _mockProjectLinksGateway.Setup(x => x.DeleteProjectLink(1));

            _projectLinksUseCase.ExecuteDelete(1);

            _mockProjectLinksGateway.Verify(x => x.DeleteProjectLink(1));
            _mockProjectLinksGateway.Verify(x => x.DeleteProjectLink(It.Is<int>(l => l == 1)), Times.Once());
        }


    }
}
