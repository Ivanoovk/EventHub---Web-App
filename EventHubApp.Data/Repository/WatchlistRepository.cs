

using EventHubApp.Data.Models;
using EventHubApp.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventHubApp.Data.Repository
{
    public class WatchlistRepository : BaseRepository<ApplicationUserEvent, object>, IWatchlistRepository
    {
        public WatchlistRepository(EventHubAppDbContext dbContext)
            : base(dbContext)
        {

        }

        public ApplicationUserEvent? GetByCompositeKey(string userId, string eventId)
        {
            return this
                .GetAllAttached()
                .SingleOrDefault(aum => aum.ApplicationUserId.ToLower() == userId.ToLower() &&
                        aum.EventId.ToString().ToLower() == eventId.ToLower());
        }

        public Task<ApplicationUserEvent?> GetByCompositeKeyAsync(string userId, string eventId)
        {
            return this
                .GetAllAttached()
                .SingleOrDefaultAsync(aum => aum.ApplicationUserId.ToLower() == userId.ToLower() &&
                        aum.EventId.ToString().ToLower() == eventId.ToLower());
        }

        public bool Exists(string userId, string eventId)
        {
            return this
                .GetAllAttached()
                .Any(aum => aum.ApplicationUserId.ToLower() == userId.ToLower() &&
                            aum.EventId.ToString().ToLower() == eventId.ToLower());
        }

        public Task<bool> ExistsAsync(string userId, string eventId)
        {
            return this
                .GetAllAttached()
                .AnyAsync(aum => aum.ApplicationUserId.ToLower() == userId.ToLower() &&
                            aum.EventId.ToString().ToLower() == eventId.ToLower());
        }
    }
}
