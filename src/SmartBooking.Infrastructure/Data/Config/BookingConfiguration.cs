using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartBooking.Core.Entities;

namespace SmartBooking.Infrastructure.Data.Config
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.BookedAt)
                   .IsRequired();

            builder.Property(b => b.Status)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(b => b.TotalAmount)
                   .HasColumnType("decimal(18,2)");

        }
    }
}
