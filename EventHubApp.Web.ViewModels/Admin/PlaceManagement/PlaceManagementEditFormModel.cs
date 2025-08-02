

using System.ComponentModel.DataAnnotations;

namespace EventHubApp.Web.ViewModels.Admin.PlaceManagement
{
    public class PlaceManagementEditFormModel : PlaceManagementAddFormModel
    {
        [Required]
        public string Id { get; set; } = null!;
    }
}
