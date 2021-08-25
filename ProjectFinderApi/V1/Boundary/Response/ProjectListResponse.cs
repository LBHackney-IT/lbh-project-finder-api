using System.Collections.Generic;

namespace ProjectFinderApi.V1.Boundary.Response
{
    public class ProjectListResponse
    {
        public List<ProjectResponse> Projects { get; set; } = null!;

        public string NextCursor { get; set; } = null!;
    }
}
