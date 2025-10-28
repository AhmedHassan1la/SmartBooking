using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartBooking.Core.Entities;

namespace SmartBooking.Infrastructure.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.OrderDate)
                   .IsRequired();

            builder.Property(o => o.Status)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.HasOne(o => o.Restaurant)     
                    .WithMany(r => r.Orders)         
                    .HasForeignKey(o => o.RestaurantId)
                    .OnDelete(DeleteBehavior.Restrict); 

           


        }
    }
}
