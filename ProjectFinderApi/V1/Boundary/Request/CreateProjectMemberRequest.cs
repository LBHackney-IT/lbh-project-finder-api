using System;
using System.Text.Json.Serialization;
using FluentValidation;

namespace ProjectFinderApi.V1.Boundary.Request
{
    public class CreateProjectMemberRequest
    {
        [JsonPropertyName("project_id")]
        public int ProjectId { get; set; }

        [JsonPropertyName("user_id")]
        public int UserId { get; set; }

        [JsonPropertyName("project_role")]
        public string ProjectRole { get; set; } = null!;

    }

    public class CreateProjectMemberRequestValidator : AbstractValidator<CreateProjectMemberRequest>
    {
        public CreateProjectMemberRequestValidator()
        {
            RuleFor(m => m.ProjectId)
            .NotNull().WithMessage("A project ID must be provided");
            RuleFor(m => m.UserId)
            .NotNull().WithMessage("A user ID must be provided");
            RuleFor(m => m.ProjectRole)
            .NotNull().WithMessage("A project role must be provided")
            .MaximumLength(100).WithMessage("Project role must be no longer than 100 characters")
            .MinimumLength(1).WithMessage("A project role must be provided");
        }
    }

}
