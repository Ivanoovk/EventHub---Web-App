

namespace EventHubApp.Web.ViewModels.Event
{
    public class AllEventsIndexViewModel
    {
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Type { get; set; } = null!;

        public string ReleaseDate { get; set; } = null!;

        public string Sponsor { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public bool IsAddedToUserWatchlist { get; set; }
    }
}
