

using EventHubApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventHubApp.Data.Configuration
{
    public class ApplicationUserEventConfiguration : IEntityTypeConfiguration<ApplicationUserEvent>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserEvent> entity)
        {
            entity
                .HasKey(aue => new { aue.ApplicationUserId, aue.EventId });

            entity
                .Property(aue => aue.ApplicationUserId)
                .IsRequired();


            entity
                .Property(aue => aue.IsDeleted)
                .HasDefaultValue(false);

            entity
                .HasOne(aue => aue.ApplicationUser)
                .WithMany() 
                .HasForeignKey(aue => aue.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasOne(aue => aue.Event)
                .WithMany(e => e.UserWatchlists)
                .HasForeignKey(aue => aue.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasQueryFilter(aum => aum.Event.IsDeleted == false);


            entity
                .HasQueryFilter(aum => aum.IsDeleted == false);
        }
    }
}
