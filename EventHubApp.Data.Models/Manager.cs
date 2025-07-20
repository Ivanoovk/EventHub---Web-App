

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventHubApp.Data.Models
{
    public class Manager
    {
        public Guid Id { get; set; }

        public bool IsDeleted { get; set; }

        public string UserId { get; set; } = null!;

        public virtual IdentityUser User { get; set; } = null!;

        public virtual ICollection<Place> ManagedPlaces { get; set; }
            = new HashSet<Place>();
    }
}
