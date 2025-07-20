
using EventHubApp.Web.ViewModels.Watchlist;

namespace EventHubApp.Services.Core.Interfaces
{
    public interface IWatchlistService
    {
        Task<IEnumerable<WatchlistViewModel>> GetUserWatchlistAsync(string userId);

        Task<bool> AddEventToUserWatchlistAsync(string? eventId, string? userId);

        Task<bool> RemoveEventFromWatchlistAsync(string? eventId, string? userId);

        Task<bool> IsEventAddedToWatchlist(string? eventId, string? userId);
    }
}
