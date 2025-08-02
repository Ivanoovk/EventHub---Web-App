

using System.ComponentModel.DataAnnotations;

namespace EventHubApp.Web.ViewModels.Admin.UserManagement
{
    public class RoleSelectionInputModel
    {
        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public string Role { get; set; } = null!;
    }
}
