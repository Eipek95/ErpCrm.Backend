using ErpCrm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ErpCrm.Persistence.Configurations
{

    public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.SKU)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.CostPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.SKU)
                .IsUnique();

            builder.HasIndex(x => x.Name);
            builder.HasIndex(x => x.CategoryId);
            builder.HasIndex(x => x.IsPopular);
            builder.HasIndex(x => x.IsActive);
            builder.HasIndex(x => x.IsDeleted);
            builder.HasIndex(x => x.CreatedDate);
        }
    }
}
