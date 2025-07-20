

using EventHubApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static EventHubApp.Data.Common.EntityConstants.PlaceEvent;

namespace EventHubApp.Data.Configuration
{
    public class PlaceEventConfiguration : IEntityTypeConfiguration<PlaceEvent>
    {
        public void Configure(EntityTypeBuilder<PlaceEvent> entity)
        {
            entity
                .HasKey(pe => pe.Id);

            entity
                .HasIndex(pe => new { pe.EventId, pe.PlaceId, pe.Showtime })
                .IsUnique(true);

            entity
                .Property(pe => pe.IsDeleted)
                .HasDefaultValue(false);

            entity
                .Property(pe => pe.AvailableTickets)
                .HasDefaultValue(AvailableTicketsDefaultValue);

            entity
                .Property(pe => pe.Showtime)
                .IsRequired(true)
                .HasMaxLength(ShowtimeMaxLength);

            entity
                .HasQueryFilter(pe => pe.IsDeleted == false &&
                                                pe.Event.IsDeleted == false &&
                                                pe.Place.IsDeleted == false);

            entity
                .HasOne(pe => pe.Event)
                .WithMany(e => e.EventProjections)
                .HasForeignKey(pe => pe.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasOne(pe => pe.Place)
                .WithMany(p => p.PlaceEvents)
                .HasForeignKey(pe => pe.PlaceId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasData(this.SeedProjections());
        }

        private IEnumerable<PlaceEvent> SeedProjections()
        {
            List<PlaceEvent> projections = new List<PlaceEvent>()
            {
                new PlaceEvent
                {
                    Id = Guid.Parse("71a411ec-d23c-4abb-b50c-75571d0a3cff"),
                    PlaceId = Guid.Parse("8a1fdfb4-08c9-44a2-a46e-0b3c45ff57b9"), // Arena 8888
                    EventId = Guid.Parse("ae50a5ab-9642-466f-b528-3cc61071bb4c"),  // Sofia Live Fest 2025
                    AvailableTickets = 10000,
                    IsDeleted = false,
                    Showtime = "18:30"
                },
                new PlaceEvent
                {
                    Id = Guid.Parse("30c505d0-9833-4087-9377-43ac8ab34e07"),
                    PlaceId = Guid.Parse("f4c3e429-0e36-47af-99a2-0c7581a7fc67"), // South Park 2
                    EventId = Guid.Parse("777634e2-3bb6-4748-8e91-7a10b70c78ac"),  // "A to JazZ 2025"
                    AvailableTickets = 1000,
                    IsDeleted = false,
                    Showtime = "13:00"
                },
                new PlaceEvent
                {
                    Id = Guid.Parse("130f6630-5593-4165-8e9e-de718ee1fb72"),
                    PlaceId = Guid.Parse("5ae6c761-1363-4a23-9965-171c70f935de"), // Vidas Art Arena
                    EventId = Guid.Parse("68fb84b9-ef2a-402f-b4fc-595006f5c275"),  // J Balvin
                    AvailableTickets = 12000,
                    IsDeleted = false,
                    Showtime = "22:15"
                },
                new PlaceEvent
                {
                    Id = Guid.Parse("c96549ed-7a19-4e83-856e-976cf306d611"),
                    PlaceId = Guid.Parse("be80d2e4-1c91-4e86-9b73-12ef08c9c3d2"), // Maimunarnika
                    EventId = Guid.Parse("02b52bb0-1c2b-49a4-ba66-6d33f81d38d1"),  // Chambo live
                    AvailableTickets = 120,
                    IsDeleted = false,
                    Showtime = "19:00"
                },
                new PlaceEvent
                {
                    Id = Guid.Parse("30864830-db09-412a-a816-6dbaccc1374c"),
                    PlaceId = Guid.Parse("33c36253-9b68-4d8a-89ae-f3276f1c3f8a"), // Vasil Levski Stadium
                    EventId = Guid.Parse("16376cc6-b3e0-4bf7-a0e4-9cbd1490522c"),  // Gun N' roses
                    AvailableTickets = 150,
                    IsDeleted = false,
                    Showtime = "17:45"
                },
                new PlaceEvent
                {
                    Id = Guid.Parse("a22c43b7-bd1d-46cd-b419-dba244e533cc"),
                    PlaceId = Guid.Parse("f4c3e429-0e36-47af-99a2-0c7581a7fc67"), // South Park 2
                    EventId = Guid.Parse("811a1a9e-61a8-4f6f-acb0-55a252c2b713"),  // Urban ART
                    AvailableTickets = 550,
                    IsDeleted = false,
                    Showtime = "17:00"
                }
            };

            return projections;
        }
    }
}
