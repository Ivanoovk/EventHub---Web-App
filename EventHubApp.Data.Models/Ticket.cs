
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventHubApp.Data.Models
{
    public class Ticket
    {
        public Guid Id { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public Guid PlaceEventId { get; set; }

        public virtual PlaceEvent PlaceEventProjection { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public virtual IdentityUser User { get; set; } = null!;
    }
}
