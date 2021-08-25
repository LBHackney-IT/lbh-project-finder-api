using System;
using ProjectFinderApi.V1.Boundary.Request;
using ProjectFinderApi.V1.Boundary.Response;
using ProjectFinderApi.V1.Gateways.Interfaces;
using ProjectFinderApi.V1.UseCase.Interfaces;
using ProjectFinderApi.V1.Factories;
using System.Linq;

namespace ProjectFinderApi.V1.UseCase
{
    public class ProjectsUseCase : IProjectsUseCase
    {
        private readonly IProjectsGateway _projectsGateway;

        public ProjectsUseCase(IProjectsGateway projectsGateway)
        {
            _projectsGateway = projectsGateway;
        }

        public ProjectResponse ExecutePost(CreateProjectRequest createProjectRequest)
        {

            return _projectsGateway.CreateProject(createProjectRequest).ToDomain().ToResponse();
        }

        public ProjectResponse? ExecuteGet(GetProjectRequest getProjectRequest)
        {
            var project = _projectsGateway.GetProjectById(getProjectRequest);
            return project?.ToDomain().ToResponse();
        }

        public void ExecutePatch(UpdateProjectRequest updateProjectRequest)
        {
            _projectsGateway.UpdateProject(updateProjectRequest);
        }

        public void ExecuteDelete(int id)
        {
            _projectsGateway.DeleteProject(id);
        }

        public ProjectListResponse ExecuteGetAllByQuery(ProjectQueryParams pqp, int cursor, int limit)
        {
            limit = limit < 10 ? 10 : limit;
            limit = limit > 100 ? 100 : limit;

            var projects = _projectsGateway.GetProjectsByQuery(cursor, limit, pqp.ProjectName, pqp.Size, pqp.Phase);

            var nextCursor = projects.Count == limit ? projects.Max(p => p.Id).ToString() : null;

            return new ProjectListResponse
            {
                Projects = projects,
                NextCursor = nextCursor
            };
        }


    }
}
