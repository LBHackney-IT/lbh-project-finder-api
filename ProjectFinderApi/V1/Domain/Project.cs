namespace ProjectFinderApi.V1.Domain
{
    public class Project
    {
        public int Id { get; set; }

        public string ProjectName { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string? ProjectContact { get; set; }

        public string Phase { get; set; } = null!;

        public string Size { get; set; } = null!;

        public string? Category { get; set; }

        public string? Priority { get; set; }
        public string? ProductUsers { get; set; }
        public string? Dependencies { get; set; }

    }
}
