using System;
using System.Linq;
using ProjectFinderApi.V1.Boundary.Request;
using ProjectFinderApi.V1.Gateways.Interfaces;
using ProjectFinderApi.V1.Infrastructure;
using ProjectFinderApi.V1.Exceptions;
using Project = ProjectFinderApi.V1.Infrastructure.Project;
using System.Collections.Generic;
using ProjectFinderApi.V1.Boundary.Response;
using ProjectFinderApi.V1.Factories;

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

        public Project GetProjectById(GetProjectRequest getProjectRequest)
        {
            return _databaseContext.Projects.Where(project => project.Id == getProjectRequest.Id).FirstOrDefault();
        }

        public void UpdateProject(UpdateProjectRequest updateProjectRequest)
        {
            var project = _databaseContext.Projects.FirstOrDefault(x => x.Id == updateProjectRequest.Id);

            if (project == null)
            {
                throw new PatchProjectException($"Project with ID {updateProjectRequest.Id} not found");
            }

            project.ProjectName = updateProjectRequest.ProjectName;
            project.Description = updateProjectRequest.Description;
            project.ProjectContact = updateProjectRequest.ProjectContact;
            project.Phase = updateProjectRequest.Phase;
            project.Size = updateProjectRequest.Size;
            project.Category = updateProjectRequest.Category;
            project.Priority = updateProjectRequest.Priority;
            project.ProductUsers = updateProjectRequest.ProductUsers;
            project.Dependencies = updateProjectRequest.Dependencies;

            _databaseContext.SaveChanges();
        }


        public void DeleteProject(int id)
        {
            var project = _databaseContext.Projects.FirstOrDefault(x => x.Id == id);

            if (project == null)
            {
                throw new DeleteProjectException($"Project with ID {id} not found");
            }

            _databaseContext.Remove(project);
            _databaseContext.SaveChanges();
        }

        public List<ProjectResponse> GetProjectsByQuery(int cursor, int limit, string? projectName = null, string? size = null, string? phase = null)
        {
            var projects = _databaseContext.Projects
            .Where(p => string.IsNullOrEmpty(projectName) || p.ProjectName.ToLower().Contains(projectName.ToLower()))
            .Where(p => string.IsNullOrEmpty(size) || p.Size.ToLower().Contains(size.ToLower()))
            .Where(p => string.IsNullOrEmpty(phase) || p.Phase.ToLower().Contains(phase.ToLower()))
            .Where(p => p.Id > cursor)
            .OrderBy(p => p.Id)
            .Take(limit);

            var response = projects.Select(x => x.ToDomain().ToResponse()).ToList();

            return response;
        }

    }

}
