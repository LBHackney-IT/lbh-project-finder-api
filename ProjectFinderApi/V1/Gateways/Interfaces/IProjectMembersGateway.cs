using System.Collections.Generic;
using ProjectFinderApi.V1.Boundary.Request;
using ProjectFinderApi.V1.Boundary.Response;

namespace ProjectFinderApi.V1.Gateways.Interfaces
{
    public interface IProjectMembersGateway
    {
        void CreateProjectMember(CreateProjectMemberRequest createProjectMemberRequest);

        List<ProjectMemberResponse> GetProjectMembersByProjectId(int projectId);
    }
}
