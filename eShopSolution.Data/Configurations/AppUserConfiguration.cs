using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopSolution.Data.Entities;


namespace eShopSolution.Data.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUsers");

            builder.Property(x => x.FirstName).HasMaxLength(200).IsRequired();

            builder.Property(x => x.LastName).HasMaxLength(200).IsRequired();

            builder.Property(x => x.Dob).IsRequired();
        }
    }
}
