using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpCrm.Persistence.Configurations
{
    public sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CompanyName)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.ContactName)
                .HasMaxLength(150);

            builder.Property(x => x.Email)
                .HasMaxLength(200);

            builder.Property(x => x.Phone)
                .HasMaxLength(30);

            builder.Property(x => x.City)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.District)
                .HasMaxLength(100);

            builder.HasIndex(x => x.CompanyName);
            builder.HasIndex(x => x.Email);
            builder.HasIndex(x => x.City);
            builder.HasIndex(x => x.IsActive);
            builder.HasIndex(x => x.IsDeleted);
            builder.HasIndex(x => x.CreatedDate);
        }
    }
}
