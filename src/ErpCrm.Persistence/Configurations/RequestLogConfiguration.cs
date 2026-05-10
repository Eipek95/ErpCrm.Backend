using ErpCrm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpCrm.Persistence.Configurations;

public class RequestLogConfiguration
    : IEntityTypeConfiguration<RequestLog>
{
    public void Configure(EntityTypeBuilder<RequestLog> builder)
    {
        builder.Property(x => x.Method)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(x => x.Path)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.IPAddress)
            .HasMaxLength(100);

        builder.Property(x => x.UserAgent)
            .HasMaxLength(1000);

        builder.Property(x => x.CorrelationId)
            .HasMaxLength(200);

        builder.HasIndex(x => x.UserId);

        builder.HasIndex(x => x.Path);

        builder.HasIndex(x => x.StatusCode);

        builder.HasIndex(x => x.CreatedDate);
    }
}