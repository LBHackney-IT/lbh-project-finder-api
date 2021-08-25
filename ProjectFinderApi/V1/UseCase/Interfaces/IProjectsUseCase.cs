using ProjectFinderApi.V1.Boundary.Request;
using ProjectFinderApi.V1.Boundary.Response;

namespace ProjectFinderApi.V1.UseCase.Interfaces
{
    public interface IProjectsUseCase
    {
        ProjectResponse ExecutePost(CreateProjectRequest request);

        ProjectResponse? ExecuteGet(GetProjectRequest request);

        void ExecutePatch(UpdateProjectRequest request);

        void ExecuteDelete(int id);
    }
}
