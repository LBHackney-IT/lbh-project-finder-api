using ProjectFinderApi.V1.Boundary.Request;
using ProjectFinderApi.V1.Boundary.Response;
using ProjectFinderApi.V1.Gateways.Interfaces;
using ProjectFinderApi.V1.UseCase.Interfaces;
using ProjectFinderApi.V1.Exceptions;
using System.Collections.Generic;

namespace ProjectFinderApi.V1.UseCase
{
    public class ProjectMembersUseCase : IProjectMembersUseCase
    {
        private readonly IProjectMembersGateway _projectMembersGateway;

        public ProjectMembersUseCase(IProjectMembersGateway projectMembersGateway)
        {
            _projectMembersGateway = projectMembersGateway;
        }

        public void ExecutePost(CreateProjectMemberRequest request)
        {
            _projectMembersGateway.CreateProjectMember(request);
        }

        public List<ProjectMemberResponse> ExecuteGetByProjectId(int projectId)
        {
            var members = _projectMembersGateway.GetProjectMembersByProjectId(projectId);

            return members;


        }

    }
}
