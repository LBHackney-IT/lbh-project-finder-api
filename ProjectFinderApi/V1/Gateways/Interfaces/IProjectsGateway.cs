using System.Collections.Generic;
using ProjectFinderApi.V1.Boundary.Request;
using ProjectFinderApi.V1.Boundary.Response;
using ProjectFinderApi.V1.Infrastructure;

namespace ProjectFinderApi.V1.Gateways.Interfaces
{
    public interface IProjectsGateway
    {
        Project CreateProject(CreateProjectRequest createProjectRequest);

        Project GetProjectById(GetProjectRequest getProjectRequest);

        void UpdateProject(UpdateProjectRequest updateProjectRequest);

    }
}
