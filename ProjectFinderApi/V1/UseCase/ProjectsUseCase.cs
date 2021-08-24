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

        public void ExecutePatch(UpdateProjectRequest updateProjectRequest)
        {
            _projectsGateway.UpdateProject(updateProjectRequest);
        }
    }
}
