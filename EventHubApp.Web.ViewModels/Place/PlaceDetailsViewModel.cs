

namespace EventHubApp.Web.ViewModels.Place
{
    public class PlaceDetailsViewModel
    {
        public string Name { get; set; } = null!;

        public string Location { get; set; } = null!;

        public IEnumerable<PlaceDetailsEventViewModel> Events { get; set; } = null!;
    }
}
