

using System.ComponentModel.DataAnnotations;

namespace EventHubApp.Web.ViewModels.Event
{
    public class DeleteEventViewModel
    {
        [Required]
        public string Id { get; set; } = null!;

        public string? Title { get; set; }

        public string? ImageUrl { get; set; }
    }
}
