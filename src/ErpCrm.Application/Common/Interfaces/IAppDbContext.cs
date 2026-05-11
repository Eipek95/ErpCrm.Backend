using ErpCrm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace ErpCrm.Application.Common.Interfaces;
public interface IAppDbContext
{
    DbSet<User> Users { get; }
    DbSet<Role> Roles { get; }
    DbSet<UserRole> UserRoles { get; }
    DbSet<Customer> Customers { get; }
    DbSet<Category> Categories { get; }
    DbSet<Product> Products { get; }
    DbSet<ProductVariant> ProductVariants { get; }
    DbSet<ProductImage> ProductImages { get; }
    DbSet<Warehouse> Warehouses { get; }
    DbSet<Stock> Stocks { get; }
    DbSet<StockMovement> StockMovements { get; }
    DbSet<Order> Orders { get; }
    DbSet<OrderItem> OrderItems { get; }
    DbSet<Payment> Payments { get; }
    DbSet<Notification> Notifications { get; }
    DbSet<AuditLog> AuditLogs { get; }
    DbSet<RequestLog> RequestLogs { get; }
    DbSet<RefreshToken> RefreshTokens { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
