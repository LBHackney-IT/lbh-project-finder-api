using System;
using System.Text.Json.Serialization;
using FluentValidation;

namespace ProjectFinderApi.V1.Boundary.Request
{
    public class CreateUserRequest
    {
        [JsonPropertyName("email_address")]
        public string EmailAddress { get; set; } = null!;

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; } = null!;

        [JsonPropertyName("last_name")]
        public string LastName { get; set; } = null!;

        [JsonPropertyName("role")]
        public string Role { get; set; } = null!;
    }

    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(u => u.EmailAddress)
            .NotNull().WithMessage("Email address must be provided")
            .MaximumLength(80).WithMessage("Email address must be no longer than 80 characters")
            .EmailAddress().WithMessage("Email address must be valid");
            RuleFor(u => u.FirstName)
            .NotNull().WithMessage("First name must be provided")
            .MaximumLength(100).WithMessage("First name must be no longer than 100 characters")
            .MinimumLength(1).WithMessage("First name must be provided");
            RuleFor(u => u.LastName)
            .NotNull().WithMessage("Last name must be provided")
            .MaximumLength(100).WithMessage("Last name must be no longer than 100 characters")
            .MinimumLength(1).WithMessage("Last name must be provided");
            RuleFor(u => u.Role)
            .NotNull().WithMessage("Role must be provided")
            .MaximumLength(70).WithMessage("Role must be no longer than 70 characters")
            .MinimumLength(1).WithMessage("Role must be provided");
        }
    }
}
