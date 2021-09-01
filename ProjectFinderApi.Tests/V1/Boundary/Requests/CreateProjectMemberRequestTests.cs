using System;
using System.Collections.Generic;
using Bogus;
using NUnit.Framework;
using FluentAssertions;
using ProjectFinderApi.Tests.V1.Helpers;
using ProjectFinderApi.V1.Boundary.Request;

namespace ProjectFinderApi.Tests.V1.Boundary.Requests
{
    [TestFixture]
    public class CreateProjectMemberRequestTests
    {

        private readonly Faker _faker = new Faker();

        [Test]
        public void CreateProjectMemberRequestValidationReturnsErrorsWithInvalidProperties()
        {

            var badCreateProjectMemberRequests = new List<(CreateProjectMemberRequest, string)>
            {
                (TestHelpers.CreateProjectMemberRequest(projectRole: _faker.Random.String2(102)), "Project role must be no longer than 100 characters"),
                (TestHelpers.CreateProjectMemberRequest(projectRole: ""), "A project role must be provided"),
            };

            var validator = new CreateProjectMemberRequestValidator();

            foreach (var (request, expectedErrorMessage) in badCreateProjectMemberRequests)
            {
                var validationResponse = validator.Validate(request);

                if (validationResponse == null)
                {
                    throw new NullReferenceException();
                }

                validationResponse.IsValid.Should().Be(false);
                validationResponse.ToString().Should().Be(expectedErrorMessage);
            }
        }

        [Test]
        public void ValidCreateProjectMemberRequestReturnsNoErrorsOnValidation()
        {
            var projectMemberRequest = TestHelpers.CreateProjectMemberRequest();
            var validator = new CreateProjectMemberRequestValidator();

            var validationResponse = validator.Validate(projectMemberRequest);

            validationResponse.IsValid.Should().Be(true);
        }
    }
}
