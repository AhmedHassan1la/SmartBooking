using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartBooking.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBooking.Infrastructure.Data.Config
{
    public class AppointmentSlotConfiguration : IEntityTypeConfiguration<AppointmentSlot>
    {
        public void Configure(EntityTypeBuilder<AppointmentSlot> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.StartTime)
                .IsRequired();

            builder.Property(a => a.EndTime)
                .IsRequired();

            builder.HasOne(s => s.Doctor)
                   .WithMany(d => d.AppointmentSlots)
                   .HasForeignKey(s => s.DoctorId)
                   .OnDelete(DeleteBehavior.Restrict);

            
            builder.HasOne(s => s.Clinic)
                   .WithMany(c => c.AppointmentSlots)
                   .HasForeignKey(s => s.ClinicId)
                   .OnDelete(DeleteBehavior.Restrict); 

        }
    }
}
