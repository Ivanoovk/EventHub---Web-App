

namespace EventHubApp.Web.ViewModels.Event
{
    public class EventDetailsViewModel
    {
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Type { get; set; } = null!;

        public string ReleaseDate { get; set; } = null!;

        public string Sponsor { get; set; } = null!;

        public int Duration { get; set; }

        public string Description { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;
    }
}
