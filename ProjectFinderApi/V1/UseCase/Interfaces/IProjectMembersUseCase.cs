using System.Collections.Generic;
using ProjectFinderApi.V1.Boundary.Request;
using ProjectFinderApi.V1.Boundary.Response;

namespace ProjectFinderApi.V1.UseCase.Interfaces
{
    public interface IProjectMembersUseCase
    {
        void ExecutePost(CreateProjectMemberRequest request);

        List<ProjectMemberResponse> ExecuteGetByProjectId(int projectId);
    }
}
