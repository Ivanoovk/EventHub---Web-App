

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventHubApp.Data.Models
{
    public class ApplicationUserEvent
    {
        [Comment("Foreign key to the referenced AspNetUser. Part of the entity composite PK.")]
        public string ApplicationUserId { get; set; } = null!;

        public virtual IdentityUser ApplicationUser { get; set; } = null!;

        [Comment("Foreign key to the referenced Event. Part of the entity composite PK.")]
        public Guid EventId { get; set; }

        public virtual Event Event { get; set; } = null!;

        [Comment("Shows if ApplicationUserEvent entry is deleted")]
        public bool IsDeleted { get; set; }
    }
}
