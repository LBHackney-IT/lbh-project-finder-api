using ProjectFinderApi.V1.Boundary.Response;
using ProjectFinderApi.V1.UseCase.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectFinderApi.V1.Exceptions;
using ProjectFinderApi.V1.Boundary.Request;
using System.Collections.Generic;

namespace ProjectFinderApi.V1.Controllers
{
    [ApiController]
    [Route("api/v1")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    public class ProjectLinkController : BaseController
    {
        private readonly IProjectLinksUseCase _projectLinksUseCase;
        public ProjectLinkController(IProjectLinksUseCase projectLinksUseCase)
        {
            _projectLinksUseCase = projectLinksUseCase;
        }

        /// <summary>
        /// Create a project link
        /// </summary>
        /// <response code="204">project link created successfully</response>
        /// <response code="400">Invalid CreateProjectLinkRequest received.</response>
        /// <response code="422">Could not process request</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPost]
        [Route("links")]
        public IActionResult CreateProjectLink([FromBody] CreateProjectLinkRequest request)
        {
            var validator = new CreateProjectLinkRequestValidator();
            var validationResults = validator.Validate(request);
            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.ToString());
            }

            try
            {
                _projectLinksUseCase.ExecutePost(request);
                return NoContent();
            }
            catch (PostProjectLinkException e)
            {
                return UnprocessableEntity(e.Message);
            }
        }

        /// <summary>
        /// get project links by project
        /// </summary>
        /// <response code="200">project links successfully found</response>
        /// <response code="422">Could not process request</response>
        [ProducesResponseType(typeof(List<ProjectLinkResponse>), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("projects/{projectId:int}/links")]
        public IActionResult GetProjectLinksByProjectId(int projectId)
        {

            var response = _projectLinksUseCase.ExecuteGet(projectId);
            if (response.Count == 0)
            {
                return NotFound("No links found for that project ID");
            }

            return Ok(response);

        }


        /// <summary>
        /// delete a project link
        /// </summary>
        /// <response code="200">project link successfully deleted</response>
        /// <response code="422">Could not process request</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete]
        [Route("links/{id:int}")]
        public IActionResult DeleteProjectLink(int id)
        {
            try
            {
                _projectLinksUseCase.ExecuteDelete(id);
                return Ok();
            }
            catch (GetProjectLinksException e)
            {
                return UnprocessableEntity(e.Message);
            }
        }
    }
}
