using System;
using ProjectFinderApi.V1.Boundary.Request;
using ProjectFinderApi.V1.Gateways.Interfaces;
using ProjectFinderApi.V1.Infrastructure;
using Project = ProjectFinderApi.V1.Infrastructure.Project;

namespace ProjectFinderApi.V1.Gateways
{
    public class ProjectsGateway : IProjectsGateway
    {
        private readonly DatabaseContext _databaseContext;

        public ProjectsGateway(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Project CreateProject(CreateProjectRequest createProjectRequest)
        {
            var project = new Project
            {
                ProjectName = createProjectRequest.ProjectName,
                Description = createProjectRequest.Description,
                ProjectContact = createProjectRequest.ProjectContact,
                Phase = createProjectRequest.Phase,
                Size = createProjectRequest.Size,
                Category = createProjectRequest.Category,
                Priority = createProjectRequest.Priority,
                ProductUsers = createProjectRequest.ProductUsers,
                Dependencies = createProjectRequest.Dependencies

            };

            _databaseContext.Projects.Add(project);
            _databaseContext.SaveChanges();
            return project;
        }
    }
}
