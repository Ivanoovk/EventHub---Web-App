
namespace EventHubApp.Data.Models
{
    public class PlaceEvent
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }

        public virtual Event Event { get; set; } = null!;

        public Guid PlaceId { get; set; }

        public virtual Place Place { get; set; } = null!;

        public int AvailableTickets { get; set; }

        public bool IsDeleted { get; set; }

        public string Showtime { get; set; } = null!;

        public virtual ICollection<Ticket> Tickets { get; set; }
            = new HashSet<Ticket>();
    }
}
