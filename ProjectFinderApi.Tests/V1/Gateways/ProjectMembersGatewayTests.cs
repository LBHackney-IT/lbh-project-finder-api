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
    public class ProjectMembersGatewayTests : DatabaseTests
    {
        private ProjectMembersGateway _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _classUnderTest = new ProjectMembersGateway(DatabaseContext);
        }

        [Test]
        public void CreateProjectMemberInsertsProjectMemberIntoDatabase()
        {
            var (user, project) = SaveUserAndProjectToDatabase(DatabaseContext);
            var request = TestHelpers.CreateProjectMemberRequest(userId: user.Id, projectId: project.Id);

            _classUnderTest.CreateProjectMember(request);

            var newMember = DatabaseContext.ProjectMembers.FirstOrDefault(x => x.ProjectId == request.ProjectId);

            newMember.Should().NotBeNull();
            newMember.ProjectId.Should().Be(request.ProjectId);
            newMember.UserId.Should().Be(request.UserId);
            newMember.ProjectRole.Should().Be(request.ProjectRole);
        }

        [Test]
        public void CreateProjectMemberThrowsPostProjectMemberExceptionIfProjectDoesNotExist()
        {
            var (user, project) = SaveUserAndProjectToDatabase(DatabaseContext);
            var request = TestHelpers.CreateProjectMemberRequest(userId: user.Id);

            Action act = () => _classUnderTest.CreateProjectMember(request);

            act.Should().Throw<PostProjectMemberException>().WithMessage($"The project with the id of {request.ProjectId} could not be found");
        }

        [Test]
        public void CreateProjectMemberThrowsPostProjectMemberExceptionIfUserDoesNotExist()
        {
            var (user, project) = SaveUserAndProjectToDatabase(DatabaseContext);
            var request = TestHelpers.CreateProjectMemberRequest(projectId: project.Id);

            Action act = () => _classUnderTest.CreateProjectMember(request);

            act.Should().Throw<PostProjectMemberException>().WithMessage($"The user with the id of {request.UserId} could not be found");
        }

        private static (User, Project) SaveUserAndProjectToDatabase(DatabaseContext context)
        {
            var user = TestHelpers.CreateUser();
            var project = TestHelpers.CreateProject();

            context.Add(user);
            context.Add(project);

            context.SaveChanges();

            return (user, project);
        }

    }
}
