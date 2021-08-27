using ProjectFinderApi.V1.Boundary.Response;
using ProjectFinderApi.V1.UseCase.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectFinderApi.V1.Exceptions;
using ProjectFinderApi.V1.Boundary.Request;

namespace ProjectFinderApi.V1.Controllers
{
    [ApiController]
    [Route("api/v1/members")]
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
    }
}
