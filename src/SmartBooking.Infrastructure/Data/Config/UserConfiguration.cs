using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartBooking.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBooking.Infrastructure.Data.Config;
public class UserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {

        builder.Property(b => b.DisplayName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(b => b.Gender)
            .HasMaxLength(20)
            .IsRequired();


        builder.Property(b => b.Address)
            .HasMaxLength(250)
            .IsRequired();


        builder.Property(b => b.ImageUrl)
            .HasMaxLength(500)
            .IsRequired();


        builder.UseTptMappingStrategy();
    }
}
