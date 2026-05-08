using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Domain.Common;
using ErpCrm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;

namespace ErpCrm.Persistence.Context
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        public AppDbContext(DbContextOptions<AppDbContext> options, ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Warehouse> Warehouses => Set<Warehouse>();
        public DbSet<Stock> Stocks => Set<Stock>();
        public DbSet<StockMovement> StockMovements => Set<StockMovement>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<Notification> Notifications => Set<Notification>();
        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

        public DbSet<ProductVariant> ProductVariants => Set<ProductVariant>();

        public DbSet<ProductImage> ProductImages => Set<ProductImage>();
        public override async Task<int> SaveChangesAsync(
    CancellationToken cancellationToken = default)
        {
            AddAuditLogs();

            return await base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            ApplySoftDeleteFilters(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void ApplySoftDeleteFilters(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Role>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Customer>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Category>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Product>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Warehouse>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Stock>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<StockMovement>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Order>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<OrderItem>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Payment>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Notification>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<AuditLog>().HasQueryFilter(x => !x.IsDeleted);
        }
        private void AddAuditLogs()
        {
            var entries = ChangeTracker.Entries<BaseEntity>()
                .Where(x =>
                    x.Entity is not AuditLog &&
                    x.State is EntityState.Added or EntityState.Modified or EntityState.Deleted)
                .ToList();

            foreach (var entry in entries)
            {
                var action = entry.State switch
                {
                    EntityState.Added => "Create",
                    EntityState.Modified => "Update",
                    EntityState.Deleted => "Delete",
                    _ => "Unknown"
                };

                var auditLog = new AuditLog
                {
                    UserId = _currentUserService.UserId,
                    Action = action,
                    EntityName = entry.Entity.GetType().Name,
                    OldValues = entry.State == EntityState.Modified
                        ? JsonSerializer.Serialize(GetOldValues(entry))
                        : null,
                    NewValues = entry.State is EntityState.Added or EntityState.Modified
                        ? JsonSerializer.Serialize(GetNewValues(entry))
                        : null,
                    CreatedDate = DateTime.UtcNow
                };

                AuditLogs.Add(auditLog);
            }
        }
        private static Dictionary<string, object?> GetOldValues(
    EntityEntry<BaseEntity> entry)
        {
            var oldValues = new Dictionary<string, object?>();

            foreach (var property in entry.Properties)
            {
                if (property.IsModified)
                {
                    oldValues[property.Metadata.Name] = property.OriginalValue;
                }
            }

            return oldValues;
        }

        private static Dictionary<string, object?> GetNewValues(
            EntityEntry<BaseEntity> entry)
        {
            var newValues = new Dictionary<string, object?>();

            foreach (var property in entry.Properties)
            {
                if (entry.State == EntityState.Added || property.IsModified)
                {
                    newValues[property.Metadata.Name] = property.CurrentValue;
                }
            }

            return newValues;
        }

    }

}
