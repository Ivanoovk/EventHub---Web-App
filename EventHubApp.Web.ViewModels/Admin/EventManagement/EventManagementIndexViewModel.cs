

namespace EventHubApp.Web.ViewModels.Admin.EventManagement
{
    public class EventManagementIndexViewModel
    {
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Type { get; set; } = null!;

        public int Duration { get; set; }

        public string Sponsor { get; set; } = null!;

        public string ReleaseDate { get; set; } = null!;

        public bool IsDeleted { get; set; }
    }
}
