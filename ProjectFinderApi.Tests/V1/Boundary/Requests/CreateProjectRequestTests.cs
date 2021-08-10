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
    public class CreateProjectRequestTests
    {

        private readonly Faker _faker = new Faker();

        [Test]
        public void CreateProjectRequestValidationReturnsErrorsWithInvalidProperties()
        {

            var badCreateProjectRequests = new List<(CreateProjectRequest, string)>
            {
                (TestHelpers.CreateProjectRequest(projectName: ""), "Project name must be provided"),
                (TestHelpers.CreateProjectRequest(description: ""), "A description must be provided"),
                (TestHelpers.CreateProjectRequest(projectContact: _faker.Random.String2(102)), "Project contact must be no longer than 100 characters"),
                (TestHelpers.CreateProjectRequest(phase: ""), "A phase must be provided"),
                (TestHelpers.CreateProjectRequest(size: ""), "Size must be provided"),
                (TestHelpers.CreateProjectRequest(category: _faker.Random.String2(72)), "Project category must be no longer than 70 characters"),
                (TestHelpers.CreateProjectRequest(priority: _faker.Random.String2(52)), "Project priority must be no longer than 50 characters"),
                (TestHelpers.CreateProjectRequest(productUsers: _faker.Random.String2(502)), "Product users must be no longer than 500 characters"),
                (TestHelpers.CreateProjectRequest(dependencies: _faker.Random.String2(302)), "Dependencies must be no longer than 300 characters"),



            };

            var validator = new CreateProjectRequestValidator();

            foreach (var (request, expectedErrorMessage) in badCreateProjectRequests)
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
        public void ValidCreateProjectRequestReturnsNoErrorsOnValidation()
        {
            var projectRequest = TestHelpers.CreateProjectRequest();
            var validator = new CreateProjectRequestValidator();

            var validationResponse = validator.Validate(projectRequest);

            validationResponse.IsValid.Should().Be(true);
        }
    }
}
