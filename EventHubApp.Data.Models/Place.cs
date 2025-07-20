

namespace EventHubApp.Data.Models
{
    public class Place
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
        public bool IsDeleted { get; set; }

        public Guid? ManagerId { get; set; }

        public virtual Manager? Manager { get; set; }

        public virtual ICollection<PlaceEvent> PlaceEvents { get; set; }
            = new HashSet<PlaceEvent>();
    }
}
