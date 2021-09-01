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
    public class ProjectLinksGateway : IProjectLinksGateway
    {
        private readonly DatabaseContext _databaseContext;

        public ProjectLinksGateway(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void CreateProjectLink(CreateProjectLinkRequest createProjectLinkRequest)
        {
            var project = _databaseContext.Projects.FirstOrDefault(x => x.Id == createProjectLinkRequest.ProjectId);
            if (project == null)
            {
                throw new PostProjectLinkException($"The project with the id of {createProjectLinkRequest.ProjectId} could not be found");
            }

            var projectlink = new ProjectLink
            {
                ProjectId = createProjectLinkRequest.ProjectId,
                LinkTitle = createProjectLinkRequest.LinkTitle,
                Link = createProjectLinkRequest.Link,

            };

            _databaseContext.ProjectLinks.Add(projectlink);
            _databaseContext.SaveChanges();
        }

        public List<ProjectLinkResponse> GetProjectLinks(int projectId)
        {
            var response = _databaseContext.ProjectLinks
            .Where(x => x.ProjectId == projectId)
            .Select(x => new ProjectLinkResponse()
            {
                Id = x.Id,
                LinkTitle = x.LinkTitle,
                Link = x.Link,
            }).ToList();

            return response;
        }

        public void DeleteProjectLink(int id)
        {
            var link = _databaseContext.ProjectLinks
          .Where(x => x.Id == id)
          .FirstOrDefault();

            if (link == null)
            {
                throw new GetProjectLinksException($"Project link with ID: {id} not found");
            }

            _databaseContext.ProjectLinks.Remove(link);
            _databaseContext.SaveChanges();
        }
    }
}
