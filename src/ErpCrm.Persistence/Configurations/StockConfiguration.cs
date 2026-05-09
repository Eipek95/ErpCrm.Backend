using ErpCrm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpCrm.Persistence.Configurations
{
    public sealed class StockConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.ToTable("Stocks");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.Property(x => x.ReservedQuantity)
                .IsRequired();

            builder.HasOne(x => x.Product)
                .WithMany(x => x.Stocks)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ProductVariant)
                .WithMany(x => x.Stocks)
                .HasForeignKey(x => x.ProductVariantId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Warehouse)
                .WithMany(x => x.Stocks)
                .HasForeignKey(x => x.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.ProductId, x.WarehouseId });

            builder.HasIndex(x => new { x.ProductVariantId, x.WarehouseId })
                .IsUnique()
                .HasFilter("[ProductVariantId] IS NOT NULL");

            builder.HasIndex(x => x.ProductId);
            builder.HasIndex(x => x.ProductVariantId);
            builder.HasIndex(x => x.WarehouseId);
            builder.HasIndex(x => x.IsDeleted);
            builder.HasIndex(x => new
            {
                x.ProductId,
                x.ProductVariantId,
                x.WarehouseId
            });

        }
    }
}
