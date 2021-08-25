using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using ProjectFinderApi.Tests.V1.Helpers;
using ProjectFinderApi.V1.Exceptions;
using ProjectFinderApi.V1.Gateways;
using ProjectFinderApi.V1.Infrastructure;

namespace ProjectFinderApi.Tests.V1.Gateways
{
    public class ProjectsGatewayTests : DatabaseTests
    {
        private ProjectsGateway _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _classUnderTest = new ProjectsGateway(DatabaseContext);
        }

        [Test]
        public void CreateProjectInsertsProjectIntoDatabase()
        {
            var createdProjectRequest = TestHelpers.CreateProjectRequest();

            var returnedProject = _classUnderTest.CreateProject(createdProjectRequest);

            returnedProject.Should().NotBeNull();
            returnedProject.ProjectName.Should().Be(createdProjectRequest.ProjectName);
            returnedProject.Description.Should().Be(createdProjectRequest.Description);
            returnedProject.ProjectContact.Should().BeNull();
            returnedProject.Phase.Should().Be(createdProjectRequest.Phase);
            returnedProject.Size.Should().Be(createdProjectRequest.Size);
            returnedProject.Category.Should().BeNull();
            returnedProject.Priority.Should().BeNull();
            returnedProject.ProductUsers.Should().BeNull();
            returnedProject.Dependencies.Should().BeNull();

        }

        private Project SaveProjectToDatabase(Project project)
        {
            DatabaseContext.Projects.Add(project);
            DatabaseContext.SaveChanges();
            return project;

        }

        [Test]
        public void GetProjectByIdReturnsNullIfNoProjectFound()
        {
            var projectRequest = TestHelpers.GetProjectRequest();
            var response = _classUnderTest.GetProjectById(projectRequest);
            response.Should().BeNull();
        }

        [Test]
        public void GetProjectByIdReturnsProject()
        {
            var projectRequest = TestHelpers.GetProjectRequest(id: 1);
            var project = SaveProjectToDatabase(TestHelpers.CreateProject(id: projectRequest.Id));

            var response = _classUnderTest.GetProjectById(projectRequest);

            response.Should().NotBeNull();
            response.Id.Should().Be(projectRequest.Id);
            response.ProjectName.Should().Be(project.ProjectName);
            response.Description.Should().Be(project.Description);
            response.ProjectContact.Should().Be(project.ProjectContact);
            response.Phase.Should().Be(project.Phase);
            response.Size.Should().Be(project.Size);
            response.Category.Should().Be(project.Category);
            response.Priority.Should().Be(project.Priority);
            response.ProductUsers.Should().Be(project.ProductUsers);
            response.Dependencies.Should().Be(project.Dependencies);
        }

        [Test]
        public void UpdateProjectThrowsExceptionIfProjectNotFound()
        {
            var updateProjectRequest = TestHelpers.UpdateProjectRequest();

            Action act = () => _classUnderTest.UpdateProject(updateProjectRequest);

            act.Should().Throw<PatchProjectException>().WithMessage($"Project with ID {updateProjectRequest.Id} not found");
        }

        [Test]
        public void UpdateProjectUpdatesAnExistingProject()
        {
            var id = 20;
            var updateProjectRequest = TestHelpers.UpdateProjectRequest(id: id);
            var project = SaveProjectToDatabase(TestHelpers.CreateProject(id: id));

            _classUnderTest.UpdateProject(updateProjectRequest);

            var updatedProject = DatabaseContext.Projects.First(p => p.Id == project.Id);

            updatedProject.ProjectName.Should().Be(updateProjectRequest.ProjectName);
            updatedProject.Description.Should().Be(updateProjectRequest.Description);
            updatedProject.ProjectContact.Should().Be(updateProjectRequest.ProjectContact);
            updatedProject.Phase.Should().Be(updateProjectRequest.Phase);
            updatedProject.Size.Should().Be(updateProjectRequest.Size);
            updatedProject.Category.Should().Be(updateProjectRequest.Category);
            updatedProject.Priority.Should().Be(updateProjectRequest.Priority);
            updatedProject.ProductUsers.Should().Be(updateProjectRequest.ProductUsers);
            updatedProject.Dependencies.Should().Be(updateProjectRequest.Dependencies);
        }

        [Test]
        public void DeleteProjectDeletesAProject()
        {
            var project = SaveProjectToDatabase(TestHelpers.CreateProject(id: 20));

            _classUnderTest.DeleteProject(20);

            var deletedProject = DatabaseContext.Projects.FirstOrDefault(p => p.Id == project.Id);

            deletedProject.Should().BeNull();
        }

    }
}
