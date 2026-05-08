using ErpCrm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpCrm.Persistence.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FullName)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.PasswordHash)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(x => x.RefreshToken)
                .HasMaxLength(500);

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.HasIndex(x => x.IsActive);
            builder.HasIndex(x => x.IsDeleted);
            builder.HasIndex(x => x.CreatedDate);
        }
    }
}
