using Microsoft.AspNetCore.Mvc;

namespace ProjectFinderApi.V1.Boundary.Request
{
    public class ProjectQueryParams
    {
        [FromQuery(Name = "project_name")]
        public string? ProjectName { get; set; }

        [FromQuery(Name = "phase")]
        public string? Phase { get; set; }

        [FromQuery(Name = "size")]
        public string? Size { get; set; }

    }
}
