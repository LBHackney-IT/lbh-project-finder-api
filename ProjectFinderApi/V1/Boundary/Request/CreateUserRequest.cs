using System;
using System.Text.Json.Serialization;
using FluentValidation;

namespace ProjectFinderApi.V1.Boundary.Request
{
    public class CreateUserRequest
    {
        [JsonPropertyName("emailAdress")]
        public string EmailAddress { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }
    }

    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(u => u.EmailAddress)
            .NotNull().WithMessage("Email address must be provided")
            .EmailAddress().WithMessage("Email address must be valid");
            RuleFor(u => u.FirstName)
            .NotNull().WithMessage("First name must be provided")
            .MinimumLength(1).WithMessage("First name must be provided");
            RuleFor(u => u.LastName)
            .NotNull().WithMessage("Last name must be provided")
            .MinimumLength(1).WithMessage("Last name must be provided");
            RuleFor(u => u.Role)
            .NotNull().WithMessage("Role must be provided")
            .MinimumLength(1).WithMessage("Role must be provided");
        }
    }
}
