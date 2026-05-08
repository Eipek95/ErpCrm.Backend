using ErpCrm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpCrm.Persistence.Configurations
{
    public sealed class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ImageUrl)
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(x => x.AltText)
                .HasMaxLength(250);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.Images)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ProductVariant)
                .WithMany(x => x.Images)
                .HasForeignKey(x => x.ProductVariantId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.ProductId);
            builder.HasIndex(x => x.ProductVariantId);
            builder.HasIndex(x => x.IsMain);
            builder.HasIndex(x => x.SortOrder);
            builder.HasIndex(x => x.IsDeleted);
        }
    }
}
