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
    public class ProductInCategoryConfiguration : IEntityTypeConfiguration<ProductInCategory>
    {
        public void Configure(EntityTypeBuilder<ProductInCategory> builder)
        {
            builder.HasKey(pc => new { pc.ProductId, pc.CategoryId }); // khóa chính

            builder.ToTable("ProductInCategories"); // tên bảng trong db

            builder.HasOne(pc => pc.Product).WithMany(p => p.ProductInCategories).HasForeignKey(pc => pc.ProductId); // khóa ngoại

            builder.HasOne(pc => pc.Category).WithMany(c => c.ProductInCategories).HasForeignKey(pc => pc.CategoryId); // khóa ngoại
        }
    }
}
