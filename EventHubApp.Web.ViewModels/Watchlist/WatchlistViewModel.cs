

namespace EventHubApp.Web.ViewModels.Watchlist
{
    public class WatchlistViewModel
    {
        public string EventId { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Type { get; set; } = null!;

        public string ReleaseDate { get; set; } = null!;

        public string? ImageUrl { get; set; } = null!;
    }
}
