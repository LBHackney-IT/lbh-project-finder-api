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
    public class UpdateProjectRequestTests
    {

        private readonly Faker _faker = new Faker();

        [Test]
        public void UpdateProjectRequestValidationReturnsErrorsWithInvalidProperties()
        {

            var badUpdateProjectRequests = new List<(UpdateProjectRequest, string)>
            {
                (TestHelpers.UpdateProjectRequest(projectName: ""), "Project name must be provided"),
                (TestHelpers.UpdateProjectRequest(description: ""), "A description must be provided"),
                (TestHelpers.UpdateProjectRequest(projectContact: _faker.Random.String2(102)), "Project contact must be no longer than 100 characters"),
                (TestHelpers.UpdateProjectRequest(phase: ""), "A phase must be provided"),
                (TestHelpers.UpdateProjectRequest(size: ""), "Size must be provided"),
                (TestHelpers.UpdateProjectRequest(category: _faker.Random.String2(72)), "Project category must be no longer than 70 characters"),
                (TestHelpers.UpdateProjectRequest(priority: _faker.Random.String2(52)), "Project priority must be no longer than 50 characters"),
                (TestHelpers.UpdateProjectRequest(productUsers: _faker.Random.String2(502)), "Product users must be no longer than 500 characters"),
                (TestHelpers.UpdateProjectRequest(dependencies: _faker.Random.String2(302)), "Dependencies must be no longer than 300 characters"),



            };

            var validator = new UpdateProjectRequestValidator();

            foreach (var (request, expectedErrorMessage) in badUpdateProjectRequests)
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
        public void ValidUpdateProjectRequestReturnsNoErrorsOnValidation()
        {
            var projectRequest = TestHelpers.UpdateProjectRequest();
            var validator = new UpdateProjectRequestValidator();

            var validationResponse = validator.Validate(projectRequest);

            validationResponse.IsValid.Should().Be(true);
        }
    }
}
