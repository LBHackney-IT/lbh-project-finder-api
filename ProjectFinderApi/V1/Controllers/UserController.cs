using ProjectFinderApi.V1.Boundary.Response;
using ProjectFinderApi.V1.UseCase.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectFinderApi.V1.Exceptions;
using ProjectFinderApi.V1.Boundary.Request;

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
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserRequest request)
        {
            try
            {
                var createdUser = _usersUseCase.ExecutePost(request);
                return CreatedAtAction("User successfully created", createdUser);
            }
            catch (PostUserException e)
            {
                return UnprocessableEntity(e.Message);
            }
        }
    }
}
