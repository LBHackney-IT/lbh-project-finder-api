using System.Collections.Generic;
using ProjecFinderApi.V1.Domain;

namespace ProjecFinderApi.V1.Gateways
{
    public interface IExampleGateway
    {
        Entity GetEntityById(int id);

        List<Entity> GetAll();
    }
}
