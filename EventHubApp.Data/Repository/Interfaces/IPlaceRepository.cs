using EventHubApp.Data.Models;


namespace EventHubApp.Data.Repository.Interfaces
{
    public interface IPlaceRepository
        : IRepository<Place, Guid>, IAsyncRepository<Place, Guid>
    {

    }
}
