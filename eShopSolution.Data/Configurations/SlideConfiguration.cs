using eShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Data.Configurations
{
	public class SlideConfiguration : IEntityTypeConfiguration<Slide>
	{
		public void Configure(EntityTypeBuilder<Slide> builder)
		{
			builder.ToTable("Slides");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).UseIdentityColumn();

			builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
			builder.Property(x => x.Description).HasMaxLength(500);
			builder.Property(x => x.Url).IsRequired().HasMaxLength(200);
			builder.Property(x => x.Image).IsRequired().HasMaxLength(200);
		}
	}
}
