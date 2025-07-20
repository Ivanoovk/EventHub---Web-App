
using Microsoft.EntityFrameworkCore;

namespace EventHubApp.Data.Models
{
    [Comment("Event in the system")]
    public class Event
    {
        [Comment("Event identifier")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Comment("Event title")]
        public string Title { get; set; } = null!;

        [Comment("Event type")]
        public string Type { get; set; } = null!;

        [Comment("Event release date")]
        public DateOnly ReleaseDate { get; set; }

        [Comment("Event sponsor")]
        public string Sponsor { get; set; } = null!;

        [Comment("Event duration")]
        public int Duration { get; set; }

        [Comment("Event description")]
        public string Description { get; set; } = null!;

        [Comment("Event image url from the image store")]
        public string? ImageUrl { get; set; }

        [Comment("Shows if Event is deleted")]
        public bool IsDeleted { get; set; }

        public virtual ICollection<ApplicationUserEvent> UserWatchlists { get; set; }
            = new HashSet<ApplicationUserEvent>();

        public virtual ICollection<PlaceEvent> EventProjections { get; set; }
            = new HashSet<PlaceEvent>();
    }
}
