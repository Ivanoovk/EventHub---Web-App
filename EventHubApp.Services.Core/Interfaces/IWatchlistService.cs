
using EventHubApp.Web.ViewModels.Watchlist;

namespace EventHubApp.Services.Core.Interfaces
{
    public interface IWatchlistService
    {
        Task<IEnumerable<WatchlistViewModel>> GetUserWatchlistAsync(string userId);

        //Task<bool> AddMovieToUserWatchlistAsync(string? movieId, string? userId);

        //Task<bool> RemoveMovieFromWatchlistAsync(string? movieId, string? userId);

        Task<bool> IsEventAddedToWatchlist(string? eventId, string? userId);
    }
}
