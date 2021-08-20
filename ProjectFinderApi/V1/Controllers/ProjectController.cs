using ProjectFinderApi.V1.Boundary.Response;
using ProjectFinderApi.V1.UseCase.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectFinderApi.V1.Exceptions;
using ProjectFinderApi.V1.Boundary.Request;

namespace ProjectFinderApi.V1.Controllers
{
    [ApiController]
    [Route("api/v1/projects")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    public class ProjectController : BaseController
    {
        private readonly IProjectsUseCase _projectsUseCase;
        public ProjectController(IProjectsUseCase projectsUseCase)
        {
            _projectsUseCase = projectsUseCase;
        }

        /// <summary>
        /// Create a project
        /// </summary>
        /// <response code="201">project created successfully</response>
        /// <response code="400">Invalid CreateProjectRequest received.</response>
        /// <response code="422">Could not process request</response>
        [ProducesResponseType(typeof(ProjectResponse), StatusCodes.Status201Created)]
        [HttpPost]
        public IActionResult CreateProject([FromBody] CreateProjectRequest request)
        {
            var validator = new CreateProjectRequestValidator();
            var validationResults = validator.Validate(request);
            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.ToString());
            }

            try
            {
                var createdProject = _projectsUseCase.ExecutePost(request);
                return CreatedAtAction("CreateProject", createdProject);
            }
            catch (PostProjectException e)
            {
                return UnprocessableEntity(e.Message);
            }
        }
    }
}
