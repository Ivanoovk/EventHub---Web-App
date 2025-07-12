

using EventHubApp.Data.Models;
using EventHubApp.Data.Repository.Interfaces;

namespace EventHubApp.Data.Repository
{
    public class EventRepository : BaseRepository<Event, Guid>, IEventRepository
    {
        public EventRepository(EventHubAppDbContext dbContext)
            : base(dbContext)
        {

        }
    }
}
