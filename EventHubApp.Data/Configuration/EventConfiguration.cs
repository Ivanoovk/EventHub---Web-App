
namespace EventHubApp.Data.Configuration
{
    using EventHubApp.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using static EventHubApp.Data.Common.EntityConstants.Event;


    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> entity)
        {
            // Define the primary key of the Event entity
            entity
                .HasKey(e => e.Id);

            // Define constraints for the Title column
            entity
                .Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(TitleMaxLength);

            // Define constraints for the Type column
            entity
                .Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(TypeMaxLength);

            // Define constraints for the ReleaseDate column
            entity
                .Property(e => e.ReleaseDate)
                .IsRequired();

            // Define constraints for the Sponsor column
            entity
                .Property(e => e.Sponsor)
                .IsRequired()
                .HasMaxLength(SponsorNameMaxLength);

            // Define constraints for the Duration column
            entity
                .Property(e => e.Duration)
                .IsRequired();

            // Define constraints for the Description column
            entity
                .Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(DescriptionMaxLength);

            // Define constraints for the ImageUrl column
            entity
                .Property(e => e.ImageUrl)
                .IsRequired(false)
                .HasMaxLength(ImageUrlMaxLength);

            // Define constraints for the IsDeleted column
            entity
                .Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            //// Filter out only the active (non-deleted) entries
            entity
                .HasQueryFilter(e => e.IsDeleted == false);


            // Seed events data with migration for development
            entity
                .HasData(this.SeedEvents());

        }

        public List<Event> SeedEvents()
        {
            List<Event> events = new List<Event>()
            {
                new Event()
                {
                    Id = Guid.Parse("ae50a5ab-9642-466f-b528-3cc61071bb4c"),
                    Title = "Sofia Live Fest 2025",
                    Type = "Entertaining",
                    ReleaseDate = new DateOnly(2025, 06, 27),
                    Sponsor = "Fan Zone",
                    Duration = 7,
                    Description = "Get ready for three unforgettable days of music, energy, and festival magic! ✨\r\n\r\nSofia Live Fest returns in 2025 for its biggest edition yet — with Massive Attack headlining June 29 in their long-awaited return to the region!",
                    ImageUrl = "https://cdn-az.allevents.in/events3/banners/cd2b2df36cf25faf4191a726e93b883ca6a8d7469b5b9372daa6ff0892064a81-rimg-w1200-h628-dcc3a5cf-gmir?v=1750226192"
                },
                new Event()
                {
                    Id = Guid.Parse("777634e2-3bb6-4748-8e91-7a10b70c78ac"),
                    Title = "A to JazZ 2025",
                    Type = "Music",
                    ReleaseDate = new DateOnly(2025, 07, 03),
                    Sponsor = "JazZ Fondation",
                    Duration = 2,
                    Description = "A to JazZ Festival 14th Edition, 3-4-5-6 July 2025, South Park 2, Sofia, Bulgaria.\r\nA to JazZ Festival is FOUR DAYS of diverse music, culture and art, fun, education, and environmental responsibility!\r\nMore info: www.atojazz.bg",
                    ImageUrl = "https://cdn-az.allevents.in/events8/banners/effe12725d73373d6229ebf5a38957238278d7f2ae2e066eb422670b44b7b131-rimg-w1200-h444-dcf08ca6-gmir?v=1750237454"
                },
                new Event()
                {
                    Id = Guid.Parse("68fb84b9-ef2a-402f-b4fc-595006f5c275"),
                    Title = "J Balvin @ Sofia Solid - Vidas Art Arena",
                    Type = "Music",
                    ReleaseDate = new DateOnly(2025, 07, 01),
                    Sponsor = "Fest Team",
                    Duration = 1,
                    Description = "On July 1, 2025, at Vidas Art Arena, Borisova Gradina, one of the most influential figures on the contemporary music scene will present a show that combines the best of Latin rhythms, urban sounds and club energy. Summer 2025 promises to be even hotter with the soundtrack of the hot Colombian star J Balvin.",
                    ImageUrl = "https://cdn-az.allevents.in/events4/banners/68d1519bb5a0c93c4aa36e8127f3d32e427a50e45a09e00b79a1e8abafdb1d4c-rimg-w1200-h628-dc030616-gmir?v=1750733582"
                },
                new Event()
                {
                    Id = Guid.Parse("02b52bb0-1c2b-49a4-ba66-6d33f81d38d1"),
                    Title = "CHAMBAO Live in Sofia",
                    Type = "Dancing",
                    ReleaseDate = new DateOnly(2025, 06, 26),
                    Sponsor = "Maimunarnika",
                    Duration = 2,
                    Description = "Spanish flamenco sensation Chambao and La Marie are returning to Bulgarian soil for a magical tour, starting in Sofia on June 26th at Maimunarnika, filled with smiles, positive energy and unforgettable moments.",
                    ImageUrl = "https://cdn-az.allevents.in/events6/banners/bd3bb8c233ade380597b152ac9841527f036a4b3873cb59bbf89b17ad8bdd15f-rimg-w1200-h628-dcdcd3c0-gmir?v=1750693364"
                },
                new Event()
                {
                    Id = Guid.Parse("16376cc6-b3e0-4bf7-a0e4-9cbd1490522c"),
                    Title = "Guns N' Roses",
                    Type = "Entertaining",
                    ReleaseDate = new DateOnly(2025, 06, 21),
                    Sponsor = "EventHub",
                    Duration = 1,
                    Description = "Guns N' Roses have announced their long-awaited return to Sofia as part of their massive tour in 2025. On July 21, Vasil Levski Stadium will transform into an arena of rock greatness, where fans will be able to experience the magic of the iconic anthems of Axl Rose, Slash and Duff McKagan.",
                    ImageUrl = "https://cdn-az.allevents.in/events9/banners/2b7fe9b1de287bd9803132ef352770734d64b76479ccc851124629a61a1e8847-rimg-w1200-h628-dc010101-gmir?v=1750650103"
                },
                new Event()
                {
                    Id = Guid.Parse("811a1a9e-61a8-4f6f-acb0-55a252c2b713"),
                    Title = "SOFIA URBAN ART: Green walls of Sofia",
                    Type = "Art",
                    ReleaseDate = new DateOnly(2025, 06, 28),
                    Sponsor = "Sofia Grafiti Tour",
                    Duration = 2,
                    Description = "Welcome to the sixth edition of Sofia Graffiti Battle, which this year will begin the transformation of the underpass at the entrance to South Park!",
                    ImageUrl = "https://cdn-az.allevents.in/events5/banners/2908baee49bb75a5e82e011b2abed4940932618e7e6050f188dd35340ee004ba-rimg-w1200-h600-dc8ebf2f-gmir?v=1750638902"
                }

            };

            return events;
        }

    }
}
