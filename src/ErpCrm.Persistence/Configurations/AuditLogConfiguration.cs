using ErpCrm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpCrm.Persistence.Configurations
{
    public sealed class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.ToTable("AuditLogs");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Action)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.EntityName)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.OldValues)
                .HasColumnType("nvarchar(max)");

            builder.Property(x => x.NewValues)
                .HasColumnType("nvarchar(max)");

            builder.Property(x => x.IpAddress)
                .HasMaxLength(60);

            builder.HasOne(x => x.User)
                .WithMany(x => x.AuditLogs)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.Action);
            builder.HasIndex(x => x.EntityName);
            builder.HasIndex(x => x.CreatedDate);
            builder.HasIndex(x => x.IsDeleted);
        }
    }
}
