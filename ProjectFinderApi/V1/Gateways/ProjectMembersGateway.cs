using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectFinderApi.V1.Boundary.Request;
using ProjectFinderApi.V1.Boundary.Response;
using ProjectFinderApi.V1.Exceptions;
using ProjectFinderApi.V1.Gateways.Interfaces;
using ProjectFinderApi.V1.Infrastructure;
using ProjectMember = ProjectFinderApi.V1.Infrastructure.ProjectMember;

namespace ProjectFinderApi.V1.Gateways
{
    public class ProjectMembersGateway : IProjectMembersGateway
    {
        private readonly DatabaseContext _databaseContext;

        public ProjectMembersGateway(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void CreateProjectMember(CreateProjectMemberRequest createProjectMemberRequest)
        {
            var project = _databaseContext.Projects.FirstOrDefault(x => x.Id == createProjectMemberRequest.ProjectId);
            if (project == null)
            {
                throw new PostProjectMemberException($"The project with the id of {createProjectMemberRequest.ProjectId} could not be found");
            }

            var user = _databaseContext.Users.FirstOrDefault(x => x.Id == createProjectMemberRequest.UserId);
            if (user == null)
            {
                throw new PostProjectMemberException($"The user with the id of {createProjectMemberRequest.UserId} could not be found");
            }

            var projectMember = new ProjectMember
            {
                ProjectId = createProjectMemberRequest.ProjectId,
                UserId = createProjectMemberRequest.UserId,
                ProjectRole = createProjectMemberRequest.ProjectRole,

            };

            _databaseContext.ProjectMembers.Add(projectMember);
            _databaseContext.SaveChanges();
        }

        public List<ProjectMemberResponse> GetProjectMembersByProjectId(int projectId)
        {

            var response = _databaseContext.ProjectMembers
            .Where(x => x.ProjectId == projectId)
            .Include(x => x.Project)
            .Include(x => x.User)
            .Select(x => new ProjectMemberResponse()
            {
                Id = x.Id,
                ProjectId = x.ProjectId,
                ProjectName = x.Project.ProjectName,
                MemberName = $"{x.User.FirstName} {x.User.LastName}",
                ProjectRole = x.ProjectRole,
            }).ToList();

            return response;
        }


        public List<ProjectMemberResponse> GetProjectMembersByUserId(int userId)
        {

            var response = _databaseContext.ProjectMembers
            .Where(x => x.UserId == userId)
            .Include(x => x.Project)
            .Include(x => x.User)
            .Select(x => new ProjectMemberResponse()
            {
                Id = x.Id,
                ProjectId = x.ProjectId,
                ProjectName = x.Project.ProjectName,
                MemberName = $"{x.User.FirstName} {x.User.LastName}",
                ProjectRole = x.ProjectRole,
            }).ToList();

            return response;
        }

        public void DeleteProjectMember(int id)
        {
            var member = _databaseContext.ProjectMembers
            .Where(x => x.Id == id)
            .FirstOrDefault();

            if (member == null)
            {
                throw new GetProjectMembersException($"Project member with ID: {id} not found");
            }

            _databaseContext.ProjectMembers.Remove(member);
            _databaseContext.SaveChanges();
        }
    }
}
