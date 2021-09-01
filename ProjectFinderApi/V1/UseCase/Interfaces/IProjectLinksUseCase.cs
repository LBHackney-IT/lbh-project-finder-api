using System.Collections.Generic;
using ProjectFinderApi.V1.Boundary.Request;
using ProjectFinderApi.V1.Boundary.Response;

namespace ProjectFinderApi.V1.UseCase.Interfaces
{
    public interface IProjectLinksUseCase
    {
        void ExecutePost(CreateProjectLinkRequest request);

        List<ProjectLinkResponse> ExecuteGet(int projectId);

        void ExecuteDelete(int id);
    }
}
