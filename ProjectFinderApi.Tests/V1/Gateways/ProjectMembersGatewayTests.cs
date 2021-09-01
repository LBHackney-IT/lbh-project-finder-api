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

        private ProjectMember SaveProjectMemberToDatabase(ProjectMember projectMember)
        {
            DatabaseContext.ProjectMembers.Add(projectMember);
            DatabaseContext.SaveChanges();
            return projectMember;

        }

        [Test]
        public void GetProjectMembersByProjectIdReturnsFoundMembers()
        {
            var (user, project) = SaveUserAndProjectToDatabase(DatabaseContext);
            var projectId = project.Id;
            var member = SaveProjectMemberToDatabase(TestHelpers.CreateProjectMember(projectId: project.Id, userId: user.Id));
            var responseMember = new ProjectMemberResponse()
            {
                Id = member.Id,
                ProjectId = member.ProjectId,
                ProjectName = member.Project.ProjectName,
                MemberName = $"{member.User.FirstName} {member.User.LastName}",
                ProjectRole = member.ProjectRole
            };

            var response = _classUnderTest.GetProjectMembersByProjectId(projectId);

            response.Count.Should().Be(1);
            response.Should().ContainEquivalentOf(responseMember);
        }

        [Test]
        public void GetProjectMembersByProjectIdReturnsAnEmptyListIfNoMembersFound()
        {
            _classUnderTest.GetProjectMembersByProjectId(1).Should().BeEmpty();
        }

        [Test]
        public void GetProjectMembersByUserIdReturnsFoundMembers()
        {
            var (user, project) = SaveUserAndProjectToDatabase(DatabaseContext);
            var userId = user.Id;
            var member = SaveProjectMemberToDatabase(TestHelpers.CreateProjectMember(userId: user.Id, projectId: project.Id));
            var responseMember = new ProjectMemberResponse()
            {
                Id = member.Id,
                ProjectId = member.ProjectId,
                ProjectName = member.Project.ProjectName,
                MemberName = $"{member.User.FirstName} {member.User.LastName}",
                ProjectRole = member.ProjectRole
            };

            var response = _classUnderTest.GetProjectMembersByUserId(userId);

            response.Count.Should().Be(1);
            response.Should().ContainEquivalentOf(responseMember);
        }

        [Test]
        public void GetProjectMembersByUserIdReturnsAnEmptyListIfNoMembersFound()
        {
            _classUnderTest.GetProjectMembersByUserId(1).Should().BeEmpty();
        }

        [Test]
        public void DeleteProjectMemberDeletesAProject()
        {
            var (user, project) = SaveUserAndProjectToDatabase(DatabaseContext);
            var member = SaveProjectMemberToDatabase(TestHelpers.CreateProjectMember(userId: user.Id, projectId: project.Id));

            _classUnderTest.DeleteProjectMember(member.Id);

            var deletedMember = DatabaseContext.ProjectMembers.FirstOrDefault(x => x.Id == member.Id);

            deletedMember.Should().BeNull();
        }

        [Test]
        public void DeleteProjectMemberThrowsGetProjectMemberExceptionIfMemberDoesNotExist()
        {
            var (user, project) = SaveUserAndProjectToDatabase(DatabaseContext);
            var member = SaveProjectMemberToDatabase(TestHelpers.CreateProjectMember(userId: user.Id, projectId: project.Id));
            var notRealMemberId = 1;

            Action act = () => _classUnderTest.DeleteProjectMember(notRealMemberId);

            act.Should().Throw<GetProjectMembersException>().WithMessage($"Project member with ID: {notRealMemberId} not found");
        }

    }
}
