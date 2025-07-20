

using EventHubApp.Data.Models;
using EventHubApp.Data.Repository;
using EventHubApp.Data.Repository.Interfaces;

namespace EventHubApp.Data.Repository
{
    public class PlaceEventRepository
        : BaseRepository<PlaceEvent, Guid>, IPlaceEventRepository
    {
        public PlaceEventRepository(EventHubAppDbContext dbContext)
            : base(dbContext)
        {

        }
    }
}