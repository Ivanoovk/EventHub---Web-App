

namespace EventHubApp.Web.ViewModels.Place
{
    public class PlaceProgramViewModel
    {
        public string PlaceId { get; set; } = null!;

        public string PlaceName { get; set; } = null!;

        public string PlaceData { get; set; } = null!;

        public IEnumerable<PlaceProgramEventViewModel> Events { get; set; } = null!;
    }
}
