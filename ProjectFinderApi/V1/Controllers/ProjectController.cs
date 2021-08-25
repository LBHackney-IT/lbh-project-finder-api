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

        /// <summary>
        /// Get a project by its ID
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="404">No project found with that ID.</response>
        [ProducesResponseType(typeof(ProjectResponse), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetProject([FromQuery] GetProjectRequest request)
        {

            var project = _projectsUseCase.ExecuteGet(request);

            if (project == null)
            {
                return NotFound("No project found with that ID");
            }

            return Ok(project);
        }


        /// <summary>
        /// Update a project
        /// </summary>
        /// <param name="request"></param>
        /// <response code="204">Project successfully updated</response>
        /// <response code="400">One or more request parameters are invalid or missing</response>
        /// <response code="422">There was a problem updating the project</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPatch]
        public IActionResult UpdateProject([FromBody] UpdateProjectRequest request)
        {
            var validator = new UpdateProjectRequestValidator();
            var validationResults = validator.Validate(request);
            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.ToString());
            }

            try
            {
                _projectsUseCase.ExecutePatch(request);
                return NoContent();
            }
            catch (PatchProjectException e)
            {
                return UnprocessableEntity(e.Message);
            }
        }

        /// <summary>
        /// Delete a project
        /// </summary>
        /// <response code="200">Project successfully deleted</response>
        /// <response code="400">Invalid request received</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteProject(int id)
        {
            try
            {
                _projectsUseCase.ExecuteDelete(id);
                return Ok();
            }
            catch (DeleteProjectException e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
