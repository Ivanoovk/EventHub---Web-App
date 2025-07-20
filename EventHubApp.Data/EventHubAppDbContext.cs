namespace EventHubApp.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Reflection;

    public class EventHubAppDbContext : IdentityDbContext
    {
        public EventHubAppDbContext(DbContextOptions<EventHubAppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Event> Events { get; set; } = null!;

        public virtual DbSet<ApplicationUserEvent> ApplicationUserEvents { get; set; } = null!;

        public virtual DbSet<Place> Places { get; set; } = null!;

        public virtual DbSet<PlaceEvent> PlacesEvents { get; set; } = null!;

        public virtual DbSet<Ticket> Tickets { get; set; } = null!;

        public virtual DbSet<Manager> Managers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
