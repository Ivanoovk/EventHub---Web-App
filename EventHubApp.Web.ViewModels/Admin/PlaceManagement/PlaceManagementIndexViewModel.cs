

namespace EventHubApp.Web.ViewModels.Admin.PlaceManagement
{
    public class PlaceManagementIndexViewModel
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Location { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public string? ManagerName { get; set; }
    }
}
