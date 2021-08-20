using ProjectFinderApi.V1.Domain;
using ProjectFinderApi.V1.Factories;
using NUnit.Framework;
using ProjectFinderApi.Tests.V1.Helpers;
using ProjectFinderApi.V1.Boundary.Response;
using FluentAssertions;

namespace ProjectFinderApi.Tests.V1.Factories
{
    public class ResponseFactoryTest
    {
        [Test]
        public void CanMapDomainUserToResponse()
        {
            var domainUser = TestHelpers.CreateUser().ToDomain();

            var expectedResponse = new UserResponse()
            {
                Id = domainUser.Id,
                FirstName = domainUser.FirstName,
                LastName = domainUser.LastName,
                Email = domainUser.Email,
                Role = domainUser.Role,
            };

            var response = domainUser.ToResponse();

            response.Should().BeEquivalentTo(expectedResponse);



        }

        [Test]
        public void CanMapDomainProjectToResponse()
        {
            var domainProject = TestHelpers.CreateProject().ToDomain();

            var expectedResponse = new ProjectResponse()
            {
                Id = domainProject.Id,
                ProjectName = domainProject.ProjectName,
                Description = domainProject.Description,
                ProjectContact = domainProject.ProjectContact,
                Phase = domainProject.Phase,
                Size = domainProject.Size,
                Category = domainProject.Category,
                Priority = domainProject.Priority,
                ProductUsers = domainProject.ProductUsers,
                Dependencies = domainProject.Dependencies
            };

            var response = domainProject.ToResponse();

            response.Should().BeEquivalentTo(expectedResponse);



        }

    }
}
