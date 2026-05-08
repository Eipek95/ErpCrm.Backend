using ErpCrm.Domain.Entities;
using ErpCrm.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Persistence.Seed;

public static class DbInitializer
{
    public static async Task SeedAsync(AppDbContext context)
    {
        await context.Database.MigrateAsync();

        if (!await context.Roles.AnyAsync())
        {
            var roles = new List<Role>
            {
                new()
                {
                    Name = "Admin",
                    CreatedDate = DateTime.UtcNow
                },
                new()
                {
                    Name = "Manager",
                    CreatedDate = DateTime.UtcNow
                },
                new()
                {
                    Name = "Employee",
                    CreatedDate = DateTime.UtcNow
                }
            };

            await context.Roles.AddRangeAsync(roles);
            await context.SaveChangesAsync();
        }
    }
}