using System;
using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ProjectFinderApi.V1.Boundary.Request
{
    public class GetUserRequest
    {
        [FromQuery(Name = "email_address")]
        public string EmailAddress { get; set; } = null!;
    }

    public class GetUserRequestValidator : AbstractValidator<GetUserRequest>
    {
        public GetUserRequestValidator()
        {
            RuleFor(u => u.EmailAddress)
            .NotNull().WithMessage("Email address must be provided")
            .MaximumLength(80).WithMessage("Email address must be no longer than 80 characters")
            .EmailAddress().WithMessage("Email address must be valid");
        }
    }
}

