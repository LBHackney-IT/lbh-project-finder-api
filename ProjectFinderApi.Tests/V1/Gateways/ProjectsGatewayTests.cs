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

        [Test]
        public void GetProjectsByQueryShouldReturnAnEmptyListIfNoProjectsFound()
        {
            _classUnderTest.GetProjectsByQuery(0, 20).Should().BeEmpty();
        }

        [Test]
        public void GetProjectsByQueryReturnsAllProjectsIfNoParamsAreSet()
        {
            var project1 = SaveProjectToDatabase(TestHelpers.CreateProject(id: 1));
            var project2 = SaveProjectToDatabase(TestHelpers.CreateProject(id: 2));
            var project3 = SaveProjectToDatabase(TestHelpers.CreateProject(id: 3));

            var response = _classUnderTest.GetProjectsByQuery(0, 20);

            response.Count.Should().Be(3);
            response.Should().ContainEquivalentOf(project1);
            response.Should().ContainEquivalentOf(project2);
            response.Should().ContainEquivalentOf(project3);
        }

        [Test]
        public void GetProjectsByQueryReturnsMatchingProjectsWithProjectNameParam()
        {
            var project1 = SaveProjectToDatabase(TestHelpers.CreateProject(projectName: "test"));
            var project2 = SaveProjectToDatabase(TestHelpers.CreateProject(projectName: "Test"));
            var project3 = SaveProjectToDatabase(TestHelpers.CreateProject(projectName: "DifferentName"));

            var response = _classUnderTest.GetProjectsByQuery(0, 20, "test");

            response.Count.Should().Be(2);
            response.Should().ContainEquivalentOf(project1);
            response.Should().ContainEquivalentOf(project2);
        }

        [Test]
        public void GetProjectsByQueryReturnsMatchingProjectsWithWildcardProjectNameParam()
        {
            var project1 = SaveProjectToDatabase(TestHelpers.CreateProject(projectName: "test"));
            var project2 = SaveProjectToDatabase(TestHelpers.CreateProject(projectName: "Test"));
            var project3 = SaveProjectToDatabase(TestHelpers.CreateProject(projectName: "DifferentName"));

            var response = _classUnderTest.GetProjectsByQuery(0, 20, "te");

            response.Count.Should().Be(2);
            response.Should().ContainEquivalentOf(project1);
            response.Should().ContainEquivalentOf(project2);
        }

        [Test]
        public void GetProjectsByQueryReturnsMatchingProjectsWithSizeParam()
        {
            var project1 = SaveProjectToDatabase(TestHelpers.CreateProject(size: "large"));
            var project2 = SaveProjectToDatabase(TestHelpers.CreateProject(size: "Large"));
            var project3 = SaveProjectToDatabase(TestHelpers.CreateProject(size: "wrongSize"));

            var response = _classUnderTest.GetProjectsByQuery(0, 20, size: "large");

            response.Count.Should().Be(2);
            response.Should().ContainEquivalentOf(project1);
            response.Should().ContainEquivalentOf(project2);
        }

        [Test]
        public void GetProjectsByQueryReturnsMatchingProjectsWithWildcardSizeParam()
        {
            var project1 = SaveProjectToDatabase(TestHelpers.CreateProject(size: "large"));
            var project2 = SaveProjectToDatabase(TestHelpers.CreateProject(size: "Large"));
            var project3 = SaveProjectToDatabase(TestHelpers.CreateProject(size: "wrongSize"));

            var response = _classUnderTest.GetProjectsByQuery(0, 20, size: "lar");

            response.Count.Should().Be(2);
            response.Should().ContainEquivalentOf(project1);
            response.Should().ContainEquivalentOf(project2);
        }

        [Test]
        public void GetProjectsByQueryReturnsMatchingProjectsWithPhaseParam()
        {
            var project1 = SaveProjectToDatabase(TestHelpers.CreateProject(phase: "discovery"));
            var project2 = SaveProjectToDatabase(TestHelpers.CreateProject(phase: "Discovery"));
            var project3 = SaveProjectToDatabase(TestHelpers.CreateProject(phase: "notRealPhase"));

            var response = _classUnderTest.GetProjectsByQuery(0, 20, phase: "discovery");

            response.Count.Should().Be(2);
            response.Should().ContainEquivalentOf(project1);
            response.Should().ContainEquivalentOf(project2);
        }

        [Test]
        public void GetProjectsByQueryReturnsMatchingProjectsWithWildcardPhaseParam()
        {
            var project1 = SaveProjectToDatabase(TestHelpers.CreateProject(phase: "discovery"));
            var project2 = SaveProjectToDatabase(TestHelpers.CreateProject(phase: "Discovery"));
            var project3 = SaveProjectToDatabase(TestHelpers.CreateProject(phase: "notRealPhase"));

            var response = _classUnderTest.GetProjectsByQuery(0, 20, phase: "dis");

            response.Count.Should().Be(2);
            response.Should().ContainEquivalentOf(project1);
            response.Should().ContainEquivalentOf(project2);
        }

        [Test]
        public void GetProjectsByQueryReturnsMatchingProjectsWithProjectNameAndSizeParams()
        {
            var project1 = SaveProjectToDatabase(TestHelpers.CreateProject(projectName: "test", size: "large"));
            var project2 = SaveProjectToDatabase(TestHelpers.CreateProject(projectName: "Test", size: "Large"));
            var project3 = SaveProjectToDatabase(TestHelpers.CreateProject(projectName: "somethingelse", size: "medium"));

            var response = _classUnderTest.GetProjectsByQuery(0, 20, projectName: "test", size: "large");

            response.Count.Should().Be(2);
            response.Should().ContainEquivalentOf(project1);
            response.Should().ContainEquivalentOf(project2);
        }

        [Test]
        public void GetProjectsByQueryReturnsMatchingProjectsWithWildcardProjectNameAndSizeParams()
        {
            var project1 = SaveProjectToDatabase(TestHelpers.CreateProject(projectName: "test", size: "large"));
            var project2 = SaveProjectToDatabase(TestHelpers.CreateProject(projectName: "Test", size: "Large"));
            var project3 = SaveProjectToDatabase(TestHelpers.CreateProject(projectName: "somethingelse", size: "medium"));

            var response = _classUnderTest.GetProjectsByQuery(0, 20, projectName: "te", size: "larg");

            response.Count.Should().Be(2);
            response.Should().ContainEquivalentOf(project1);
            response.Should().ContainEquivalentOf(project2);
        }

        [Test]
        public void GetProjectsByQueryReturnsMatchingProjectsWithPhaseAndSizeParams()
        {
            var project1 = SaveProjectToDatabase(TestHelpers.CreateProject(phase: "discovery", size: "large"));
            var project2 = SaveProjectToDatabase(TestHelpers.CreateProject(phase: "Discovery", size: "Large"));
            var project3 = SaveProjectToDatabase(TestHelpers.CreateProject(phase: "somethingelse", size: "medium"));

            var response = _classUnderTest.GetProjectsByQuery(0, 20, phase: "discovery", size: "large");

            response.Count.Should().Be(2);
            response.Should().ContainEquivalentOf(project1);
            response.Should().ContainEquivalentOf(project2);
        }

        [Test]
        public void GetProjectsByQueryReturnsMatchingProjectsWithWildcardPhaseAndSizeParams()
        {
            var project1 = SaveProjectToDatabase(TestHelpers.CreateProject(phase: "discovery", size: "large"));
            var project2 = SaveProjectToDatabase(TestHelpers.CreateProject(phase: "Discovery", size: "Large"));
            var project3 = SaveProjectToDatabase(TestHelpers.CreateProject(phase: "somethingelse", size: "medium"));

            var response = _classUnderTest.GetProjectsByQuery(0, 20, phase: "disco", size: "lar");

            response.Count.Should().Be(2);
            response.Should().ContainEquivalentOf(project1);
            response.Should().ContainEquivalentOf(project2);
        }

        [Test]
        public void GetProjectsByQueryReturnsMatchingProjectsWithProjectNameAndPhaseParams()
        {
            var project1 = SaveProjectToDatabase(TestHelpers.CreateProject(projectName: "test", phase: "discovery"));
            var project2 = SaveProjectToDatabase(TestHelpers.CreateProject(projectName: "Test", phase: "Discovery"));
            var project3 = SaveProjectToDatabase(TestHelpers.CreateProject(projectName: "otherproject", phase: "maintenance"));

            var response = _classUnderTest.GetProjectsByQuery(0, 20, projectName: "test", phase: "discovery");

            response.Count.Should().Be(2);
            response.Should().ContainEquivalentOf(project1);
            response.Should().ContainEquivalentOf(project2);
        }

        [Test]
        public void GetProjectsByQueryReturnsMatchingProjectsWithWildcardProjectNameAndPhaseParams()
        {
            var project1 = SaveProjectToDatabase(TestHelpers.CreateProject(projectName: "test", phase: "discovery"));
            var project2 = SaveProjectToDatabase(TestHelpers.CreateProject(projectName: "Test", phase: "Discovery"));
            var project3 = SaveProjectToDatabase(TestHelpers.CreateProject(projectName: "otherproject", phase: "maintenance"));

            var response = _classUnderTest.GetProjectsByQuery(0, 20, projectName: "te", phase: "discov");

            response.Count.Should().Be(2);
            response.Should().ContainEquivalentOf(project1);
            response.Should().ContainEquivalentOf(project2);
        }

        [Test]
        public void GetProjectsByQueryReturnsMatchingProjectsWithAllThreeParams()
        {
            var project1 = SaveProjectToDatabase(TestHelpers.CreateProject(projectName: "test", phase: "discovery", size: "large"));
            var project2 = SaveProjectToDatabase(TestHelpers.CreateProject(projectName: "Test", phase: "Discovery", size: "Large"));
            var project3 = SaveProjectToDatabase(TestHelpers.CreateProject(projectName: "otherproject", phase: "maintenance", size: "small"));

            var response = _classUnderTest.GetProjectsByQuery(0, 20, projectName: "test", phase: "discovery", size: "large");

            response.Count.Should().Be(2);
            response.Should().ContainEquivalentOf(project1);
            response.Should().ContainEquivalentOf(project2);
        }

        [Test]
        public void GetProjectsByQueryReturnsTheNumberOfProjectsBasedOffTheLimit()
        {
            var project1 = SaveProjectToDatabase(TestHelpers.CreateProject());
            var project2 = SaveProjectToDatabase(TestHelpers.CreateProject());
            var project3 = SaveProjectToDatabase(TestHelpers.CreateProject());
            var project4 = SaveProjectToDatabase(TestHelpers.CreateProject());
            var project5 = SaveProjectToDatabase(TestHelpers.CreateProject());

            var response = _classUnderTest.GetProjectsByQuery(0, 3);

            response.Count.Should().Be(3);
        }

        [Test]
        public void GetProjectsByQueryWillOffsetResultsByCursor()
        {
            var project1 = SaveProjectToDatabase(TestHelpers.CreateProject(id: 1));
            var project2 = SaveProjectToDatabase(TestHelpers.CreateProject(id: 2));
            var project3 = SaveProjectToDatabase(TestHelpers.CreateProject(id: 3));
            var project4 = SaveProjectToDatabase(TestHelpers.CreateProject(id: 4));
            var project5 = SaveProjectToDatabase(TestHelpers.CreateProject(id: 5));

            var response = _classUnderTest.GetProjectsByQuery(2, 3);

            response.Count.Should().Be(3);
            response.First().Should().BeEquivalentTo(project3);
            response.Last().Should().BeEquivalentTo(project5);

        }
    }
}
