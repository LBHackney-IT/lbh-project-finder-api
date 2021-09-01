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
            var users = _projectMembersGateway.GetProjectMembersByUserId(request.UserId);
            var userOnProject = users.Find(project => project.ProjectId == request.ProjectId) == null;
            if (!userOnProject) throw new PostProjectMemberException($"The user with the id of {request.UserId} is already assigned to the project");

            _projectMembersGateway.CreateProjectMember(request);
        }

        public List<ProjectMemberResponse> ExecuteGetByProjectId(int projectId)
        {
            var members = _projectMembersGateway.GetProjectMembersByProjectId(projectId);

            return members;


        }

        public List<ProjectMemberResponse> ExecuteGetByUserId(int userId)
        {


            var members = _projectMembersGateway.GetProjectMembersByUserId(userId);

            return members;

        }

        public void ExecuteDelete(int id)
        {
            _projectMembersGateway.DeleteProjectMember(id);
        }

    }
}
