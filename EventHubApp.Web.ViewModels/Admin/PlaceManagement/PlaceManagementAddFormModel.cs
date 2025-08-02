
using System.ComponentModel.DataAnnotations;

using static EventHubApp.Data.Common.EntityConstants.Place;
using static EventHubApp.Web.ViewModels.ValidationMessages.Place;

namespace EventHubApp.Web.ViewModels.Admin.PlaceManagement
{
    public class PlaceManagementAddFormModel
    {
        [Required(ErrorMessage = NameRequiredMessage)]
        [MinLength(NameMinLength, ErrorMessage = NameMinLengthMessage)]
        [MaxLength(NameMaxLength, ErrorMessage = NameMaxLengthMessage)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = LocationRequiredMessage)]
        [MinLength(LocationMinLength, ErrorMessage = LocationMinLengthMessage)]
        [MaxLength(LocationMaxLength, ErrorMessage = LocationMaxLengthMessage)]
        public string Location { get; set; } = null!;

        public IEnumerable<string>? AppManagerEmails { get; set; }

        [Required]
        public string ManagerEmail { get; set; } = null!;
    }
}
