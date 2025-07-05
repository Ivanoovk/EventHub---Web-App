

using EventHubApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventHubApp.Data.Configuration
{
    public class ApplicationUserEventConfiguration : IEntityTypeConfiguration<ApplicationUserEvent>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserEvent> entity)
        {
            // Define composite Primary Key of the Mapping Entity
            entity
                .HasKey(aue => new { aue.ApplicationUserId, aue.EventId });

            // Define required constraint for the ApplicationUserId, as it is type string
            entity
                .Property(aue => aue.ApplicationUserId)
                .IsRequired();

            // Define default value for soft-delete functionality
            entity
                .Property(aue => aue.IsDeleted)
                .HasDefaultValue(false);

            // Configure relation between ApplicationUserMovie and IdentityUser
            // The IdentityUser does not contain navigation property, as it is built-in type from the ASP.NET Core Identity
            entity
                .HasOne(aue => aue.ApplicationUser)
                .WithMany() // We do not have navigation property from the IdentityUser side
                .HasForeignKey(aue => aue.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure relation between ApplicationUserMovie and Movie
            entity
                .HasOne(aue => aue.Event)
                .WithMany(e => e.UserWatchlists)
                .HasForeignKey(aue => aue.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            // Define query filter to hide the ApplicationUserMovie entries referring deleted Movie
            // Solves the problem with relations during delete
            entity
                .HasQueryFilter(aum => aum.Event.IsDeleted == false);

            // Define query filter to hide the deleted entries in the user Watchlist
            entity
                .HasQueryFilter(aum => aum.IsDeleted == false);
        }
    }
}
