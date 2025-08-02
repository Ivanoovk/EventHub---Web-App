using Microsoft.AspNetCore.Identity;


namespace EventHubApp.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual Manager? Manager { get; set; }

        public virtual ICollection<ApplicationUserEvent> WatchlistEvents { get; set; }
            = new HashSet<ApplicationUserEvent>();

        public virtual ICollection<Ticket> Tickets { get; set; }
            = new HashSet<Ticket>();
    }
}
