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
    public class GetUserRequestTests
    {
        [Test]
        public void GetUserRequestValidationReturnsErrorsWithInvalidProperties()
        {
            var longEmail = "thisEmailIsLongerWayThan80CharactersAndAlsoValid@HereIAmJustCreatingMoreCharactersToPadOutTheWordLength.com";
            var badGetUserRequests = new List<(GetUserRequest, string)>
            {
                (TestHelpers.CreateGetUserRequest(email: ""), "Email address must be valid"),
                (TestHelpers.CreateGetUserRequest(email: "somethingthatisntaemail"), "Email address must be valid"),
                (TestHelpers.CreateGetUserRequest(email: longEmail), "Email address must be no longer than 80 characters"),
            };

            var validator = new GetUserRequestValidator();

            foreach (var (request, expectedErrorMessage) in badGetUserRequests)
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
        public void ValidGetUserRequestReturnsNoErrorsOnValidation()
        {
            var userRequest = TestHelpers.CreateGetUserRequest();
            var validator = new GetUserRequestValidator();

            var validationResponse = validator.Validate(userRequest);

            validationResponse.IsValid.Should().Be(true);
        }
    }
}
