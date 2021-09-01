namespace ProjectFinderApi.V1.Boundary.Response
{
    public class ProjectMemberResponse
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string ProjectName { get; set; } = null!;

        public string MemberName { get; set; } = null!;

        public string ProjectRole { get; set; } = null!;

    }
}
