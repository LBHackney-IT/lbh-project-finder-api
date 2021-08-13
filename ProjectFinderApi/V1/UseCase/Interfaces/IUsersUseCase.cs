using System.Collections.Generic;
using ProjectFinderApi.V1.Boundary.Request;
using ProjectFinderApi.V1.Boundary.Response;

namespace ProjectFinderApi.V1.UseCase.Interfaces
{
    public interface IUsersUseCase
    {
        UserResponse ExecutePost(CreateUserRequest request);

        List<UserResponse> ExecuteGetAll();
    }
}
