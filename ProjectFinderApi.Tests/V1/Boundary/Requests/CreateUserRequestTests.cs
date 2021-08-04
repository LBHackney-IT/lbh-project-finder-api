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
    public class CreateUserRequestTests
    {

        private readonly Faker _faker = new Faker();

        [Test]
        public void CreateUserRequestValidationReturnsErrorsWithInvalidProperties()
        {
            var longEmail = "thisEmailIsLongerWayThan80CharactersAndAlsoValid@HereIAmJustCreatingMoreCharactersToPadOutTheWordLength.com";
            var badCreateUserRequests = new List<(CreateUserRequest, string)>
            {
                (TestHelpers.CreateUserRequest(email: ""), "Email address must be valid"),
                (TestHelpers.CreateUserRequest(firstName: ""), "First name must be provided"),
                (TestHelpers.CreateUserRequest(lastName: ""), "Last name must be provided"),
                (TestHelpers.CreateUserRequest(role: ""), "Role must be provided"),
                (TestHelpers.CreateUserRequest(email: longEmail), "Email address must be no longer than 80 characters"),
                (TestHelpers.CreateUserRequest(firstName: _faker.Random.String2(101)), "First name must be no longer than 100 characters"),
                (TestHelpers.CreateUserRequest(lastName: _faker.Random.String2(101)), "Last name must be no longer than 100 characters"),
                (TestHelpers.CreateUserRequest(role: _faker.Random.String2(75)), "Role must be no longer than 70 characters"),

            };

            var validator = new CreateUserRequestValidator();

            foreach (var (request, expectedErrorMessage) in badCreateUserRequests)
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
        public void ValidCreateUserRequestReturnsNoErrorsOnValidation()
        {
            var userRequest = TestHelpers.CreateUserRequest();
            var validator = new CreateUserRequestValidator();

            var validationResponse = validator.Validate(userRequest);

            validationResponse.IsValid.Should().Be(true);
        }
    }
}
