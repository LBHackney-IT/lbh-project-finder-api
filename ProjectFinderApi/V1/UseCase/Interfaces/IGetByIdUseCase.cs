using ProjecFinderApi.V1.Boundary.Response;

namespace ProjecFinderApi.V1.UseCase.Interfaces
{
    public interface IGetByIdUseCase
    {
        ResponseObject Execute(int id);
    }
}
