
using EventHubApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static EventHubApp.GCommon.ApplicationConstants;

namespace EventHubApp.Data.Configuration
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> entity)
        {
            entity
                .HasKey(t => t.Id);

            entity
                .Property(t => t.Price)
                .HasColumnType(PriceSqlType);

            entity
                .Property(t => t.UserId)
                .IsRequired(true);

            entity
                .HasOne(t => t.PlaceEventProjection)
                .WithMany(cm => cm.Tickets)
                .HasForeignKey(t => t.PlaceEventId);

            entity
                .HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId);

            entity
                .HasIndex(t => new { t.PlaceEventId, t.UserId })
                .IsUnique(true);

            entity
                .HasQueryFilter(t => t.PlaceEventProjection.IsDeleted == false);
        }
    }
}
