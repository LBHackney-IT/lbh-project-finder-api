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
    public class ProjectMemberController : BaseController
    {
        private readonly IProjectMembersUseCase _projectMembersUseCase;
        public ProjectMemberController(IProjectMembersUseCase projectMembersUseCase)
        {
            _projectMembersUseCase = projectMembersUseCase;
        }

        /// <summary>
        /// Create a project member
        /// </summary>
        /// <response code="204">project member created successfully</response>
        /// <response code="400">Invalid CreateProjectMemberRequest received.</response>
        /// <response code="422">Could not process request</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPost]
        [Route("members")]
        public IActionResult CreateProjectMember([FromBody] CreateProjectMemberRequest request)
        {
            var validator = new CreateProjectMemberRequestValidator();
            var validationResults = validator.Validate(request);
            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.ToString());
            }

            try
            {
                _projectMembersUseCase.ExecutePost(request);
                return NoContent();
            }
            catch (PostProjectMemberException e)
            {
                return UnprocessableEntity(e.Message);
            }
        }

        /// <summary>
        /// Get a project member by project Id
        /// </summary>
        /// <response code="200">project members successfully found</response>
        /// <response code="400">Invalid GetProjectMembersByProjectIdRequest received.</response>
        /// <response code="404">No project members found for this project Id</response>
        [ProducesResponseType(typeof(List<ProjectMemberResponse>), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("projects/{projectId:int}/team")]
        public IActionResult GetProjectMembersByProjectId(int projectId)
        {

            var response = _projectMembersUseCase.ExecuteGetByProjectId(projectId);
            if (response.Count == 0)
            {
                return NotFound("No members found for that project ID");
            }


            return Ok(response);

        }

        /// <summary>
        /// Get a project member by user Id
        /// </summary>
        /// <response code="200">project members successfully found</response>
        /// <response code="400">Invalid GetProjectMembersByUserIdRequest received.</response>
        /// <response code="404">No project members found for this user Id</response>
        [ProducesResponseType(typeof(List<ProjectMemberResponse>), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("user/{userId:int}/projects")]
        public IActionResult GetProjectMembersByUserId(int userId)
        {

            var response = _projectMembersUseCase.ExecuteGetByUserId(userId);
            if (response.Count == 0)
            {
                return NotFound("No members found for that user ID");
            }
            return Ok(response);
        }

        /// <summary>
        /// delete a project member
        /// </summary>
        /// <response code="200">project members successfully deleted</response>
        /// <response code="422">Could not process request</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete]
        [Route("members/{id:int}")]
        public IActionResult DeleteProjectMember(int id)
        {
            try
            {
                _projectMembersUseCase.ExecuteDelete(id);
                return Ok();
            }
            catch (GetProjectMembersException e)
            {
                return UnprocessableEntity(e.Message);
            }
        }
    }
}
