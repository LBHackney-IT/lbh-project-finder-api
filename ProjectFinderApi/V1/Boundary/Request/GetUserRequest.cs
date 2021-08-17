using System;
using System.Text.Json.Serialization;
using FluentValidation;

namespace ProjectFinderApi.V1.Boundary.Request
{
    public class GetUserRequest
    {
        [JsonPropertyName("emailAddress")]
        public string EmailAddress { get; set; } = null!;
    }

    public class GetUserRequestValidator : AbstractValidator<GetUserRequest>
    {
        public GetUserRequestValidator()
        {
            RuleFor(u => u.EmailAddress)
            .NotNull().WithMessage("Email address must be provided")
            .EmailAddress().WithMessage("Email address must be valid");
        }
    }
}

