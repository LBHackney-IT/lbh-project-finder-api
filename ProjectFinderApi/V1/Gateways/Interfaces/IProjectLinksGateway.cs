using System.Collections.Generic;
using ProjectFinderApi.V1.Boundary.Request;
using ProjectFinderApi.V1.Boundary.Response;

namespace ProjectFinderApi.V1.Gateways.Interfaces
{
    public interface IProjectLinksGateway
    {
        void CreateProjectLink(CreateProjectLinkRequest createProjectLinkRequest);

        List<ProjectLinkResponse> GetProjectLinks(int projectId);

        void DeleteProjectLink(int id);

    }
}
