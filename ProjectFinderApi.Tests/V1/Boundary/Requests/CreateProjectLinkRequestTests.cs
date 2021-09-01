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
    public class CreateProjectLinkRequestTests
    {

        private readonly Faker _faker = new Faker();

        [Test]
        public void CreateProjectLinkRequestValidationReturnsErrorsWithInvalidProperties()
        {

            var badCreateProjectLinkRequests = new List<(CreateProjectLinkRequest, string)>
            {
                (TestHelpers.CreateProjectLinkRequest(linkTitle: ""), "A link title must be provided"),
                (TestHelpers.CreateProjectLinkRequest(linkTitle: _faker.Random.String2(102)), "A link title must be no longer than 100 characters"),
                (TestHelpers.CreateProjectLinkRequest(link: ""), "A link must be provided"),
                (TestHelpers.CreateProjectLinkRequest(link: _faker.Random.String2(1002)), "A link must be no longer than 1000 characters"),

            };

            var validator = new CreateProjectLinkRequestValidator();

            foreach (var (request, expectedErrorMessage) in badCreateProjectLinkRequests)
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
        public void ValidCreateProjectLinkRequestReturnsNoErrorsOnValidation()
        {
            var projectLinkRequest = TestHelpers.CreateProjectLinkRequest();
            var validator = new CreateProjectLinkRequestValidator();

            var validationResponse = validator.Validate(projectLinkRequest);

            validationResponse.IsValid.Should().Be(true);
        }
    }
}
