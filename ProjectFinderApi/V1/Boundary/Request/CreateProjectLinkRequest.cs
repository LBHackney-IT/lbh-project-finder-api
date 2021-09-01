using System;
using System.Text.Json.Serialization;
using FluentValidation;

namespace ProjectFinderApi.V1.Boundary.Request
{
    public class CreateProjectLinkRequest
    {
        [JsonPropertyName("project_id")]
        public int ProjectId { get; set; }

        [JsonPropertyName("link_title")]
        public string LinkTitle { get; set; } = null!;

        [JsonPropertyName("link")]
        public string Link { get; set; } = null!;

    }

    public class CreateProjectLinkRequestValidator : AbstractValidator<CreateProjectLinkRequest>
    {
        public CreateProjectLinkRequestValidator()
        {
            RuleFor(m => m.ProjectId)
            .NotNull().WithMessage("A project ID must be provided");
            RuleFor(m => m.LinkTitle)
            .NotNull().WithMessage("A link title must be provided")
            .MaximumLength(100).WithMessage("A link title must be no longer than 100 characters")
            .MinimumLength(1).WithMessage("A link title must be provided");
            RuleFor(m => m.Link)
            .NotNull().WithMessage("A link must be provided")
            .MaximumLength(1000).WithMessage("A link must be no longer than 1000 characters")
            .MinimumLength(1).WithMessage("A link must be provided");
        }
    }
}
