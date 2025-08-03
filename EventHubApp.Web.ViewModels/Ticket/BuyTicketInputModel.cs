

using System.ComponentModel.DataAnnotations;

namespace EventHubApp.Web.ViewModels.Ticket
{
    public class BuyTicketInputModel
    {
        [Required]
        public string PlaceId { get; set; } = null!;

        [Required]
        public string EventId { get; set; } = null!;

        public int Quantity { get; set; }

        [Required]
        public string Showtime { get; set; } = null!;
    }
}
