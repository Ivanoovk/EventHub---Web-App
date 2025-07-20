

using EventHubApp.Data.Models;

namespace EventHubApp.Data.Repository.Interfaces
{
    public interface IPlaceEventRepository
        : IRepository<PlaceEvent, Guid>, IAsyncRepository<PlaceEvent, Guid>
    {

    }
}
