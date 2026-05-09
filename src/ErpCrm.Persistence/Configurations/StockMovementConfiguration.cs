using ErpCrm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpCrm.Persistence.Configurations
{
    public sealed class StockMovementConfiguration : IEntityTypeConfiguration<StockMovement>
    {
        public void Configure(EntityTypeBuilder<StockMovement> builder)
        {
            builder.ToTable("StockMovements");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ReferenceNumber)
                .HasMaxLength(100);

            builder.Property(x => x.Description)
                .HasMaxLength(500);

            builder.HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Warehouse)
                .WithMany(x => x.StockMovements)
                .HasForeignKey(x => x.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.ProductId);
            builder.HasIndex(x => x.WarehouseId);
            builder.HasIndex(x => x.MovementType);
            builder.HasIndex(x => x.CreatedDate);
            builder.HasIndex(x => x.IsDeleted);
            builder.HasIndex(x => x.ReferenceNumber);
        }
    }
}
