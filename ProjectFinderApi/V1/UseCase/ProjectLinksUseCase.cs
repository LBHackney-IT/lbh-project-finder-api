using ProjectFinderApi.V1.Boundary.Request;
using ProjectFinderApi.V1.Boundary.Response;
using ProjectFinderApi.V1.Gateways.Interfaces;
using ProjectFinderApi.V1.UseCase.Interfaces;
using ProjectFinderApi.V1.Exceptions;
using System.Collections.Generic;

namespace ProjectFinderApi.V1.UseCase
{
    public class ProjectLinksUseCase : IProjectLinksUseCase
    {
        private readonly IProjectLinksGateway _projectLinksGateway;

        public ProjectLinksUseCase(IProjectLinksGateway projectLinksGateway)
        {
            _projectLinksGateway = projectLinksGateway;
        }

        public void ExecutePost(CreateProjectLinkRequest request)
        {

            _projectLinksGateway.CreateProjectLink(request);
        }

        public List<ProjectLinkResponse> ExecuteGet(int projectId)
        {
            var links = _projectLinksGateway.GetProjectLinks(projectId);

            return links;
        }

        public void ExecuteDelete(int id)
        {
            _projectLinksGateway.DeleteProjectLink(id);
        }

    }
}
