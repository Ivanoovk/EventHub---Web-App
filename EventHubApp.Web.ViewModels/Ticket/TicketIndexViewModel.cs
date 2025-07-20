

namespace EventHubApp.Web.ViewModels.Ticket
{
    public class TicketIndexViewModel
    {
        public string EventTitle { get; set; } = null!;

        public string EventImageUrl { get; set; } = null!;

        public string PlaceName { get; set; } = null!;

        public string Showtime { get; set; } = null!;

        public int TicketCount { get; set; }

        public string TicketPrice { get; set; } = null!;

        public string TotalPrice { get; set; } = null!;
    }
}
