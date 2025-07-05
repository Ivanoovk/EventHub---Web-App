
using EventHubApp.Data;
using EventHubApp.Data.Models;
using EventHubApp.Services.Core.Interfaces;
using EventHubApp.Web.ViewModels.Watchlist;
using Microsoft.EntityFrameworkCore;
using static EventHubApp.GCommon.ApplicationConstants;

namespace EventHubApp.Services.Core
{
    public class WatchlistService : IWatchlistService
    {
        private readonly EventHubAppDbContext dbContext;

        public WatchlistService(EventHubAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<WatchlistViewModel>> GetUserWatchlistAsync(string userId)
        {

            IEnumerable<WatchlistViewModel> userWatchlist = await this.dbContext
                .ApplicationUserEvents
                .Include(aum => aum.Event)
                .AsNoTracking()
                .Where(aum => aum.ApplicationUserId.ToLower() == userId.ToLower())
                .Select(aum => new WatchlistViewModel()
                {
                    EventId = aum.EventId.ToString(),
                    Title = aum.Event.Title,
                    Type = aum.Event.Type,
                    ReleaseDate = aum.Event.ReleaseDate.ToString(AppDateFormat),
                    ImageUrl = aum.Event.ImageUrl ?? $"/images/{NoImageUrl}"
                })
                .ToArrayAsync();

            return userWatchlist;
        }



        public async Task<bool> IsEventAddedToWatchlist(string? eventId, string? userId)
        {
            bool result = false;
            if (eventId != null && userId != null)
            {
                bool isEventIdValid = Guid.TryParse(eventId, out Guid eventGuid);
                if (isEventIdValid)
                {
                    ApplicationUserEvent? userEventEntry = await this.dbContext
                        .ApplicationUserEvents
                        .SingleOrDefaultAsync(aum => aum.ApplicationUserId.ToLower() == userId &&
                                                     aum.EventId.ToString() == eventGuid.ToString());
                    if (userEventEntry != null)
                    {
                        result = true;
                    }
                }
            }

            return result;
        }




    }
}
