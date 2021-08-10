using System;
using System.Text.Json.Serialization;
using FluentValidation;

namespace ProjectFinderApi.V1.Boundary.Request
{
    public class CreateProjectRequest
    {
        [JsonPropertyName("project_name")]
        public string ProjectName { get; set; } = null!;

        [JsonPropertyName("description")]
        public string Description { get; set; } = null!;

        [JsonPropertyName("project_contact")]
        public string? ProjectContact { get; set; } = null!;

        [JsonPropertyName("phase")]
        public string Phase { get; set; } = null!;

        [JsonPropertyName("size")]
        public string Size { get; set; } = null!;

        [JsonPropertyName("category")]
        public string? Category { get; set; }

        [JsonPropertyName("priority")]
        public string? Priority { get; set; }

        [JsonPropertyName("product_users")]
        public string? ProductUsers { get; set; }

        [JsonPropertyName("dependencies")]
        public string? Dependencies { get; set; }
    }

    public class CreateProjectRequestValidator : AbstractValidator<CreateProjectRequest>
    {
        public CreateProjectRequestValidator()
        {
            RuleFor(u => u.ProjectName)
            .NotNull().WithMessage("Project name must be provided")
            .MaximumLength(500).WithMessage("Project name must be no longer than 500 characters")
            .MinimumLength(1).WithMessage("Project name must be provided");
            RuleFor(u => u.Description)
            .NotNull().WithMessage("A description must be provided")
            .MaximumLength(1000).WithMessage("A description must be no longer than 1000 characters")
            .MinimumLength(1).WithMessage("A description must be provided");
            RuleFor(u => u.ProjectContact)
            .MaximumLength(100).WithMessage("Project contact must be no longer than 100 characters");
            RuleFor(u => u.Phase)
            .NotNull().WithMessage("A phase must be provided")
            .MaximumLength(70).WithMessage("A phase must be no longer than 70 characters")
            .MinimumLength(1).WithMessage("A phase must be provided");
            RuleFor(u => u.Size)
            .NotNull().WithMessage("Size must be provided")
            .MaximumLength(70).WithMessage("Size must be no longer than 70 characters")
            .MinimumLength(1).WithMessage("Size must be provided");
            RuleFor(u => u.Category)
            .MaximumLength(70).WithMessage("Project category must be no longer than 70 characters");
            RuleFor(u => u.Priority)
            .MaximumLength(50).WithMessage("Project priority must be no longer than 50 characters");
            RuleFor(u => u.ProductUsers)
            .MaximumLength(500).WithMessage("Product users must be no longer than 500 characters");
            RuleFor(u => u.Dependencies)
           .MaximumLength(300).WithMessage("Dependencies must be no longer than 300 characters");
        }
    }
}
