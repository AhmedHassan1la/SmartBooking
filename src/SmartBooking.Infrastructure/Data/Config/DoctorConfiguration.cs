using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartBooking.Core.Entities;

namespace SmartBooking.Infrastructure.Data.Config
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.Property(d => d.Bio).HasMaxLength(500);
            builder.Property(d => d.Certifications).HasMaxLength(300);
            builder.Property(d => d.Education).HasMaxLength(300);

            builder.HasOne(d => d.Clinic)
                    .WithMany(c => c.Doctors)
                    .HasForeignKey(d => d.ClinicId)
                    .OnDelete(DeleteBehavior.Restrict);

           
            builder.HasOne(d => d.Speciality)
                   .WithMany(s => s.Doctors)
                   .HasForeignKey(d => d.SpecialityId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}