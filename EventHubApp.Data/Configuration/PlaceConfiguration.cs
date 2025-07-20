

using EventHubApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static EventHubApp.Data.Common.EntityConstants.Place;

namespace EventHubApp.Data.Configuration
{
    public class PlaceConfiguration : IEntityTypeConfiguration<Place>
    {
        public void Configure(EntityTypeBuilder<Place> entity)
        {
            entity
                .HasKey(p => p.Id);

            entity
                .Property(p => p.Name)
                .IsRequired(true)
                .HasMaxLength(NameMaxLength);

            entity
                .Property(p => p.Location)
                .IsRequired(true)
                .HasMaxLength(LocationMaxLength);

            entity
                .Property(p => p.IsDeleted)
                .HasDefaultValue(false);

            entity
                .HasOne(p => p.Manager)
                .WithMany(m => m.ManagedPlaces)
                .HasForeignKey(c => c.ManagerId)
                .OnDelete(DeleteBehavior.SetNull);

            entity
                .HasIndex(p => new { p.Name, p.Location })
                .IsUnique(true);

            entity
                .HasQueryFilter(p => p.IsDeleted == false);

            entity
                .HasData(this.SeedPlaces());
        }

        private IEnumerable<Place> SeedPlaces()
        {
            List<Place> places = new List<Place>()
            {
                new Place
                {
                    Id = Guid.Parse("8a1fdfb4-08c9-44a2-a46e-0b3c45ff57b9"),
                    Name = "Arena 8888",
                    Location = "Sofia, Bulgaria",
                    ManagerId = null,
                    IsDeleted = false,
                },
                new Place   
                {
                    Id = Guid.Parse("f4c3e429-0e36-47af-99a2-0c7581a7fc67"),
                    Name = "South Park 2",
                    Location = "Sofia, Bulgaria",
                    ManagerId = null,
                    IsDeleted = false,
                },
                new Place
                {
                    Id = Guid.Parse("5ae6c761-1363-4a23-9965-171c70f935de"),
                    Name = "Vidas Art Arena",
                    Location = "Sofia, Bulgaria",
                    ManagerId = null,
                    IsDeleted = false,
                },
                new Place
                {
                    Id = Guid.Parse("be80d2e4-1c91-4e86-9b73-12ef08c9c3d2"),
                    Name = "Maimunarnika",
                    Location = "Sofia, Bulgaria",
                    ManagerId = null,
                    IsDeleted = false,
                },
                new Place
                {
                    Id = Guid.Parse("33c36253-9b68-4d8a-89ae-f3276f1c3f8a"),
                    Name = "Vasil Levski Stadium",
                    Location = "Sofia, Bulgaria",
                    ManagerId = null,
                    IsDeleted = false,
                },
                new Place
                {
                    Id = Guid.Parse("844d9abd-104d-41ab-b14a-ce059779ad91"),
                    Name = "The Collodrum",
                    Location = "Plovdiv, Bulgaria",
                    ManagerId = null,
                    IsDeleted = false,
                },
                new Place
                {
                    Id = Guid.Parse("54082f99-023b-4d68-89ac-44c00a0958d0"),
                    Name = "The Sea Garden",
                    Location = "Varna, Bulgaria",
                    ManagerId = null,
                    IsDeleted = false,
                }
            };

            return places;
        }
    }
}
