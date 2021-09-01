using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using ProjectFinderApi.Tests.V1.Helpers;
using ProjectFinderApi.V1.Boundary.Response;
using ProjectFinderApi.V1.Exceptions;
using ProjectFinderApi.V1.Gateways;
using ProjectFinderApi.V1.Infrastructure;

namespace ProjectFinderApi.Tests.V1.Gateways
{
    public class ProjectLinksGatewayTests : DatabaseTests
    {
        private ProjectLinksGateway _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _classUnderTest = new ProjectLinksGateway(DatabaseContext);
        }

        [Test]
        public void CreateProjectLinkInsertsProjectLinkIntoDatabase()
        {
            var project = SaveProjectToDatabase(DatabaseContext);
            var request = TestHelpers.CreateProjectLinkRequest(projectId: project.Id);

            _classUnderTest.CreateProjectLink(request);

            var newLink = DatabaseContext.ProjectLinks.FirstOrDefault(x => x.ProjectId == request.ProjectId);

            newLink.Should().NotBeNull();
            newLink.ProjectId.Should().Be(request.ProjectId);
            newLink.LinkTitle.Should().Be(request.LinkTitle);
            newLink.Link.Should().Be(request.Link);
        }

        [Test]
        public void CreateProjectLinkThrowsPostProjectLinkExceptionIfProjectDoesNotExist()
        {
            var project = SaveProjectToDatabase(DatabaseContext);
            var request = TestHelpers.CreateProjectLinkRequest();

            Action act = () => _classUnderTest.CreateProjectLink(request);

            act.Should().Throw<PostProjectLinkException>().WithMessage($"The project with the id of {request.ProjectId} could not be found");
        }

        private static Project SaveProjectToDatabase(DatabaseContext context)
        {
            var project = TestHelpers.CreateProject();
            context.Add(project);

            context.SaveChanges();

            return project;
        }

        private ProjectLink SaveProjectLinkToDatabase(ProjectLink projectLink)
        {
            DatabaseContext.ProjectLinks.Add(projectLink);
            DatabaseContext.SaveChanges();
            return projectLink;
        }

        [Test]
        public void GetProjectLinksReturnsFoundLinks()
        {
            var project = SaveProjectToDatabase(DatabaseContext);
            var link = SaveProjectLinkToDatabase(TestHelpers.CreateProjectLink(projectId: project.Id));
            var linkResponse = new ProjectLinkResponse() { Id = link.Id, LinkTitle = link.LinkTitle, Link = link.Link };

            var response = _classUnderTest.GetProjectLinks(project.Id);

            response.Count.Should().Be(1);
            response.Should().ContainEquivalentOf(linkResponse);
        }

        [Test]
        public void GetProjectLinksReturnsAnEmptyListWhenNoLinksFound()
        {
            _classUnderTest.GetProjectLinks(1).Should().BeEmpty();
        }

        [Test]
        public void DeleteProjectLinkDeletesALink()
        {
            var project = SaveProjectToDatabase(DatabaseContext);
            var link = SaveProjectLinkToDatabase(TestHelpers.CreateProjectLink(projectId: project.Id));

            _classUnderTest.DeleteProjectLink(link.Id);

            var deletedLink = DatabaseContext.ProjectLinks.FirstOrDefault(x => x.Id == link.Id);

            deletedLink.Should().BeNull();
        }

        [Test]
        public void DeleteProjectLinkThrowsGetProjectLinkExceptionIfLinkDoesNotExist()
        {
            var project = SaveProjectToDatabase(DatabaseContext);
            var link = SaveProjectLinkToDatabase(TestHelpers.CreateProjectLink(projectId: project.Id));
            var notRealLinkId = 1;

            Action act = () => _classUnderTest.DeleteProjectLink(notRealLinkId);

            act.Should().Throw<GetProjectLinksException>().WithMessage($"Project link with ID: {notRealLinkId} not found");
        }


    }
}
