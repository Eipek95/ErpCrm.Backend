using ErpCrm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpCrm.Persistence.Configurations
{
    public sealed class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
    {
        public void Configure(EntityTypeBuilder<ProductVariant> builder)
        {
            builder.ToTable("ProductVariants");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.VariantCode)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Color)
                .HasMaxLength(80);

            builder.Property(x => x.Size)
                .HasMaxLength(50);

            builder.Property(x => x.Price)
                .HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.Product)
                .WithMany(x => x.Variants)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.VariantCode)
                .IsUnique();

            builder.HasIndex(x => x.ProductId);
            builder.HasIndex(x => x.Color);
            builder.HasIndex(x => x.Size);
            builder.HasIndex(x => x.IsActive);
            builder.HasIndex(x => x.IsDeleted);
        }
    }
}
