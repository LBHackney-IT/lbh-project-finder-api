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
    [Route("api/v1/users")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    public class UserController : BaseController
    {
        private readonly IUsersUseCase _usersUseCase;
        public UserController(IUsersUseCase usersUseCase)
        {
            _usersUseCase = usersUseCase;
        }

        /// <summary>
        /// Create a user
        /// </summary>
        /// <response code="201">User created successfully</response>
        /// <response code="400">Invalid CreateUserRequest received.</response>
        /// <response code="422">Could not process request</response>
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserRequest request)
        {
            var validator = new CreateUserRequestValidator();
            var validationResults = validator.Validate(request);
            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.ToString());
            }

            try
            {
                var createdUser = _usersUseCase.ExecutePost(request);
                return CreatedAtAction("CreateUser", createdUser);
            }
            catch (PostUserException e)
            {
                return UnprocessableEntity(e.Message);
            }
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <response code="404">No users found.</response>
        [ProducesResponseType(typeof(List<UserResponse>), StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _usersUseCase.ExecuteGetAll();
            if (users.Count == 0)
            {
                return NotFound("No users found");
            }

            return Ok(users);
        }
    }
}
