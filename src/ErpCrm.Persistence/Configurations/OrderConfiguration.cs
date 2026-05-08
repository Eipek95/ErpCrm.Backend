using ErpCrm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpCrm.Persistence.Configurations
{
    public sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.OrderNumber)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.TotalAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.CreatedByUser)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.OrderNumber)
                .IsUnique();

            builder.HasIndex(x => x.CustomerId);
            builder.HasIndex(x => x.CreatedByUserId);
            builder.HasIndex(x => x.Status);
            builder.HasIndex(x => x.CreatedDate);
            builder.HasIndex(x => x.IsDeleted);
        }
    }
}
