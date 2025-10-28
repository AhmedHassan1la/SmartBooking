using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartBooking.Core.Entities;

namespace SmartBooking.Infrastructure.Data.Config
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.FirstName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(a => a.LastName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(a => a.Street)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(a => a.City)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(a => a.State)
                   .HasMaxLength(100);

            builder.Property(a => a.ZipCode)
                   .HasMaxLength(20);

            

        }
    }
}
