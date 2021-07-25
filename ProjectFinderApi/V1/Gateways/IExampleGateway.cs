using System.Collections.Generic;
using ProjectFinderApi.V1.Domain;

namespace ProjectFinderApi.V1.Gateways
{
    public interface IExampleGateway
    {
        Entity GetEntityById(int id);

        List<Entity> GetAll();
    }
}
