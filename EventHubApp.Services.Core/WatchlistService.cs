using EventHubApp.Data.Models;
using EventHubApp.Data.Repository.Interfaces;
using EventHubApp.Services.Core.Interfaces;
using EventHubApp.Web.ViewModels.Watchlist;
using Microsoft.EntityFrameworkCore;
using static EventHubApp.GCommon.ApplicationConstants;

namespace EventHubApp.Services.Core
{
    public class WatchlistService : IWatchlistService
    {
        private readonly IWatchlistRepository watchlistRepository;

        public WatchlistService(IWatchlistRepository watchlistRepository)
        {
            this.watchlistRepository = watchlistRepository;
        }

        public async Task<IEnumerable<WatchlistViewModel>> GetUserWatchlistAsync(string userId)
        {
            // Due to the use of the built-in IdentityUser, we do not have direct navigation collection from the user side
            IEnumerable<WatchlistViewModel> userWatchlist = await this.watchlistRepository
                .GetAllAttached()
                .Include(aue => aue.Event)
                .AsNoTracking()
                .Where(aue => aue.ApplicationUserId.ToLower() == userId.ToLower())
                .Select(aue => new WatchlistViewModel()
                {
                    EventId = aue.EventId.ToString(),
                    Title = aue.Event.Title,
                    Type = aue.Event.Type,
                    ReleaseDate = aue.Event.ReleaseDate.ToString(AppDateFormat),
                    ImageUrl = aue.Event.ImageUrl ?? $"/images/{NoImageUrl}"
                })
                .ToArrayAsync();

            return userWatchlist;
        }

        public async Task<bool> AddEventToUserWatchlistAsync(string? eventId, string? userId)
        {
            bool result = false;
            if (eventId != null && userId != null)
            {
                bool isEventIdValid = Guid.TryParse(eventId, out Guid eventGuid);
                if (isEventIdValid)
                {
                    ApplicationUserEvent? userEventEntry = await this.watchlistRepository
                        .GetAllAttached()
                        .IgnoreQueryFilters()
                        .SingleOrDefaultAsync(aue => aue.ApplicationUserId.ToLower() == userId &&
                                                     aue.EventId.ToString() == eventGuid.ToString());
                    if (userEventEntry != null)
                    {
                        userEventEntry.IsDeleted = false;
                        result =
                            await this.watchlistRepository.UpdateAsync(userEventEntry);
                    }
                    else
                    {
                        userEventEntry = new ApplicationUserEvent()
                        {
                            ApplicationUserId = userId,
                            EventId = eventGuid,
                        };

                        await this.watchlistRepository.AddAsync(userEventEntry);
                        result = true;
                    }
                }
            }

            return result;
        }

        public async Task<bool> RemoveEventFromWatchlistAsync(string? eventId, string? userId)
        {
            bool result = false;
            if (eventId != null && userId != null)
            {
                bool isEventIdValid = Guid.TryParse(eventId, out Guid eventGuid);
                if (isEventIdValid)
                {
                    ApplicationUserEvent? userEventEntry = await this.watchlistRepository
                        .SingleOrDefaultAsync(aue => aue.ApplicationUserId.ToLower() == userId &&
                                                     aue.EventId.ToString() == eventGuid.ToString());
                    if (userEventEntry != null)
                    {
                        result =
                            await this.watchlistRepository.DeleteAsync(userEventEntry);
                    }
                }
            }

            return result;
        }

        public async Task<bool> IsEventAddedToWatchlist(string? eventId, string? userId)
        {
            bool result = false;
            if (eventId != null && userId != null)
            {
                bool isEventIdValid = Guid.TryParse(eventId, out Guid eventGuid);
                if (isEventIdValid)
                {
                    ApplicationUserEvent? userEventEntry = await this.watchlistRepository
                        .SingleOrDefaultAsync(aue => aue.ApplicationUserId.ToLower() == userId &&
                                                     aue.EventId.ToString() == eventGuid.ToString());
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
