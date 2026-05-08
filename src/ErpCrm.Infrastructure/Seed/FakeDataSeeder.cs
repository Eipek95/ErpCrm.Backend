using Bogus;
using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Domain.Entities;
using ErpCrm.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Infrastructure.Seed;

public class FakeDataSeeder : IFakeDataSeeder
{
    private readonly IAppDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public FakeDataSeeder(
        IAppDbContext context,
        IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        await SeedRolesAsync(cancellationToken);

        var userCount = await _context.Users.CountAsync(cancellationToken);
        if (userCount < 100)
            await SeedUsersAsync(100 - userCount, cancellationToken);

        if (!await _context.Customers.AnyAsync(cancellationToken))
            await SeedCustomersAsync(200, cancellationToken);

        if (!await _context.Categories.AnyAsync(cancellationToken))
            await SeedCategoriesAsync(cancellationToken);

        if (!await _context.Products.AnyAsync(cancellationToken))
            await SeedProductsAsync(500, cancellationToken);

        if (!await _context.Warehouses.AnyAsync(cancellationToken))
            await SeedWarehousesAsync(cancellationToken);

        if (!await _context.Stocks.AnyAsync(cancellationToken))
            await SeedStocksAsync(cancellationToken);

        if (!await _context.Orders.AnyAsync(cancellationToken))
            await SeedOrdersAsync(2000, cancellationToken);
    }

    private async Task SeedRolesAsync(CancellationToken cancellationToken)
    {
        var existingRoleNames = await _context.Roles
            .Select(x => x.Name)
            .ToListAsync(cancellationToken);

        var roleNames = new[] { "Admin", "Manager", "Employee" };

        foreach (var roleName in roleNames.Where(x => !existingRoleNames.Contains(x)))
        {
            await _context.Roles.AddAsync(new Role
            {
                Name = roleName,
                CreatedDate = DateTime.UtcNow
            }, cancellationToken);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task SeedUsersAsync(int count, CancellationToken cancellationToken)
    {
        if (count <= 0)
            return;

        var roles = await _context.Roles.ToListAsync(cancellationToken);

        var adminRole = roles.First(x => x.Name == "Admin");
        var managerRole = roles.First(x => x.Name == "Manager");
        var employeeRole = roles.First(x => x.Name == "Employee");

        var passwordHash = _passwordHasher.Hash("Admin123*");

        var existingEmails = await _context.Users
            .Select(x => x.Email)
            .ToListAsync(cancellationToken);

        var existingEmailSet = existingEmails
            .Select(x => x.ToLower())
            .ToHashSet();

        var usersToAdd = new List<User>();

        var adminEmail = "admin@erpcrm.com";

        if (!existingEmailSet.Contains(adminEmail))
        {
            var admin = new User
            {
                FullName = "System Admin",
                Email = adminEmail,
                PasswordHash = passwordHash,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };

            admin.UserRoles.Add(new UserRole
            {
                User = admin,
                Role = adminRole
            });

            usersToAdd.Add(admin);
            existingEmailSet.Add(adminEmail);
            count--;
        }

        var faker = new Faker<User>("tr")
            .RuleFor(x => x.FullName, f => f.Name.FullName())
            .RuleFor(x => x.Email, f => f.Internet.Email().ToLower())
            .RuleFor(x => x.PasswordHash, _ => passwordHash)
            .RuleFor(x => x.IsActive, f => f.Random.Bool(0.92f))
            .RuleFor(x => x.CreatedDate, f => f.Date.Past(2).ToUniversalTime());

        var fakeUserCount = 0;

        while (fakeUserCount < count)
        {
            var user = faker.Generate();
            var email = user.Email.ToLower();

            if (existingEmailSet.Contains(email))
                continue;

            user.Email = email;

            var selectedRole = Random.Shared.Next(1, 100) switch
            {
                <= 10 => managerRole,
                _ => employeeRole
            };

            user.UserRoles.Add(new UserRole
            {
                User = user,
                Role = selectedRole
            });

            usersToAdd.Add(user);
            existingEmailSet.Add(email);
            fakeUserCount++;
        }

        if (usersToAdd.Any())
        {
            await _context.Users.AddRangeAsync(usersToAdd, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    private async Task SeedCustomersAsync(int count, CancellationToken cancellationToken)
    {
        var cities = new[]
        {
            "İstanbul", "Tekirdağ", "İzmir", "Ankara", "Bursa",
            "Kocaeli", "Konya", "Gaziantep", "Kayseri", "Antalya"
        };

        var sectors = new[]
        {
            "Tekstil", "Gıda", "Lojistik", "Makine", "Yazılım",
            "Mobilya", "Ambalaj", "Otomotiv", "Kimya", "Perakende"
        };

        var faker = new Faker<Customer>("tr")
            .RuleFor(x => x.CompanyName, f =>
                $"{f.Company.CompanyName()} {f.PickRandom(sectors)}")
            .RuleFor(x => x.ContactName, f => f.Name.FullName())
            .RuleFor(x => x.Email, f => f.Internet.Email().ToLower())
            .RuleFor(x => x.Phone, f => f.Phone.PhoneNumber("+90 5## ### ## ##"))
            .RuleFor(x => x.City, f => f.PickRandom(cities))
            .RuleFor(x => x.District, f => f.Address.CitySuffix())
            .RuleFor(x => x.IsActive, f => f.Random.Bool(0.82f))
            .RuleFor(x => x.CreatedDate, f => f.Date.Past(3).ToUniversalTime());

        var customers = faker.Generate(count);

        await _context.Customers.AddRangeAsync(customers, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task SeedCategoriesAsync(CancellationToken cancellationToken)
    {
        var names = new[]
        {
            "Elektronik", "Ofis Malzemeleri", "Tekstil Ürünleri", "Gıda Ürünleri",
            "Ambalaj", "Endüstriyel Ürünler", "Makine Parçaları", "Temizlik Ürünleri",
            "Mobilya", "Yedek Parça", "Kırtasiye", "Kimyasal Ürünler"
        };

        var categories = names.Select(name => new Category
        {
            Name = name,
            Description = $"{name} kategorisi",
            CreatedDate = DateTime.UtcNow
        }).ToList();

        await _context.Categories.AddRangeAsync(categories, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task SeedProductsAsync(int count, CancellationToken cancellationToken)
    {
        var categories = await _context.Categories.ToListAsync(cancellationToken);

        var faker = new Faker<Product>("tr")
            .RuleFor(x => x.Name, f => f.Commerce.ProductName())
            .RuleFor(x => x.SKU, f => $"SKU-{f.Random.AlphaNumeric(10).ToUpper()}")
            .RuleFor(x => x.Price, f => Math.Round(f.Random.Decimal(50, 5000), 2))
            .RuleFor(x => x.CostPrice, f => Math.Round(f.Random.Decimal(20, 3000), 2))
            .RuleFor(x => x.IsPopular, f => f.Random.Bool(0.18f))
            .RuleFor(x => x.IsActive, f => f.Random.Bool(0.95f))
            .RuleFor(x => x.CategoryId, f => f.PickRandom(categories).Id)
            .RuleFor(x => x.CreatedDate, f => f.Date.Past(2).ToUniversalTime());

        var products = faker.Generate(count);

        await _context.Products.AddRangeAsync(products, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        await SeedProductVariantsAndImagesAsync(cancellationToken);
    }

    private async Task SeedProductVariantsAndImagesAsync(CancellationToken cancellationToken)
    {
        var products = await _context.Products.ToListAsync(cancellationToken);
        var colors = new[] { "Siyah", "Beyaz", "Mavi", "Kırmızı", "Gri" };
        var sizes = new[] { "XS", "S", "M", "L", "XL" };

        var variants = new List<ProductVariant>();
        var images = new List<ProductImage>();

        foreach (var product in products.Where(_ => Random.Shared.NextDouble() < 0.35))
        {
            var variantCount = Random.Shared.Next(2, 5);

            for (var i = 0; i < variantCount; i++)
            {
                variants.Add(new ProductVariant
                {
                    ProductId = product.Id,
                    VariantCode = $"{product.SKU}-V{i + 1}",
                    Color = colors[Random.Shared.Next(colors.Length)],
                    Size = sizes[Random.Shared.Next(sizes.Length)],
                    Price = product.Price + Random.Shared.Next(-100, 250),
                    StockQuantity = Random.Shared.Next(20, 500),
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow
                });
            }

            images.Add(new ProductImage
            {
                ProductId = product.Id,
                ImageUrl = $"https://dummyimage.com/600x400/000/fff&text={Uri.EscapeDataString(product.Name)}",
                AltText = product.Name,
                IsMain = true,
                SortOrder = 1,
                CreatedDate = DateTime.UtcNow
            });
        }

        await _context.ProductVariants.AddRangeAsync(variants, cancellationToken);
        await _context.ProductImages.AddRangeAsync(images, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task SeedWarehousesAsync(CancellationToken cancellationToken)
    {
        var warehouses = new List<Warehouse>
        {
            new() { Name = "İstanbul Ana Depo", City = "İstanbul", Address = "Avrupa Yakası Lojistik Merkezi", IsActive = true, CreatedDate = DateTime.UtcNow },
            new() { Name = "Tekirdağ Bölge Depo", City = "Tekirdağ", Address = "Çorlu Sanayi Bölgesi", IsActive = true, CreatedDate = DateTime.UtcNow },
            new() { Name = "İzmir Ege Depo", City = "İzmir", Address = "Kemalpaşa OSB", IsActive = true, CreatedDate = DateTime.UtcNow },
            new() { Name = "Ankara Merkez Depo", City = "Ankara", Address = "Sincan OSB", IsActive = true, CreatedDate = DateTime.UtcNow },
            new() { Name = "Bursa Marmara Depo", City = "Bursa", Address = "Nilüfer OSB", IsActive = true, CreatedDate = DateTime.UtcNow }
        };

        await _context.Warehouses.AddRangeAsync(warehouses, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task SeedStocksAsync(CancellationToken cancellationToken)
    {
        var products = await _context.Products.ToListAsync(cancellationToken);
        var warehouses = await _context.Warehouses.ToListAsync(cancellationToken);
        var variants = await _context.ProductVariants.ToListAsync(cancellationToken);

        var stocks = new List<Stock>();

        foreach (var product in products)
        {
            var selectedWarehouses = warehouses
                .OrderBy(_ => Guid.NewGuid())
                .Take(Random.Shared.Next(1, Math.Min(warehouses.Count, 3)))
                .ToList();

            var productVariants = variants.Where(x => x.ProductId == product.Id).ToList();

            foreach (var warehouse in selectedWarehouses)
            {
                if (productVariants.Any())
                {
                    foreach (var variant in productVariants)
                    {
                        var quantity = Random.Shared.Next(20, 1000);

                        stocks.Add(new Stock
                        {
                            ProductId = product.Id,
                            ProductVariantId = variant.Id,
                            WarehouseId = warehouse.Id,
                            Quantity = quantity,
                            ReservedQuantity = Random.Shared.Next(0, Math.Min(quantity, 50)),
                            CreatedDate = DateTime.UtcNow
                        });
                    }
                }
                else
                {
                    var quantity = Random.Shared.Next(20, 1000);

                    stocks.Add(new Stock
                    {
                        ProductId = product.Id,
                        WarehouseId = warehouse.Id,
                        Quantity = quantity,
                        ReservedQuantity = Random.Shared.Next(0, Math.Min(quantity, 50)),
                        CreatedDate = DateTime.UtcNow
                    });
                }
            }
        }

        await _context.Stocks.AddRangeAsync(stocks, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task SeedOrdersAsync(int count, CancellationToken cancellationToken)
    {
        var customers = await _context.Customers.Where(x => x.IsActive).ToListAsync(cancellationToken);
        var users = await _context.Users.Where(x => x.IsActive).ToListAsync(cancellationToken);
        var warehouses = await _context.Warehouses.ToListAsync(cancellationToken);
        var stocks = await _context.Stocks
            .Include(x => x.Product)
            .Include(x => x.ProductVariant)
            .ToListAsync(cancellationToken);

        var orders = new List<Order>();
        var payments = new List<Payment>();
        var stockMovements = new List<StockMovement>();
        var notifications = new List<Notification>();

        for (var i = 0; i < count; i++)
        {
            var createdDate = GenerateRealisticOrderDate();
            var customer = customers[Random.Shared.Next(customers.Count)];
            var user = users[Random.Shared.Next(users.Count)];
            var warehouse = warehouses[Random.Shared.Next(warehouses.Count)];

            var availableStocks = stocks
                .Where(x => x.WarehouseId == warehouse.Id && x.Quantity - x.ReservedQuantity > 5)
                .OrderBy(_ => Guid.NewGuid())
                .Take(Random.Shared.Next(1, 5))
                .ToList();

            if (!availableStocks.Any())
                continue;

            var order = new Order
            {
                OrderNumber = $"ORD-{createdDate:yyyyMMdd}-{i + 1:D6}",
                CustomerId = customer.Id,
                CreatedByUserId = user.Id,
                Status = PickOrderStatus(),
                CreatedDate = createdDate
            };

            decimal totalAmount = 0;

            foreach (var stock in availableStocks)
            {
                var maxQuantity = Math.Min(8, stock.Quantity - stock.ReservedQuantity);

                if (maxQuantity <= 1)
                    continue;

                var quantity = Random.Shared.Next(1, maxQuantity);
                var unitPrice = stock.ProductVariant?.Price ?? stock.Product.Price;
                var totalPrice = unitPrice * quantity;

                order.Items.Add(new OrderItem
                {
                    ProductId = stock.ProductId,
                    ProductVariantId = stock.ProductVariantId,
                    Quantity = quantity,
                    UnitPrice = unitPrice,
                    TotalPrice = totalPrice,
                    CreatedDate = createdDate
                });

                stock.Quantity -= quantity;
                stock.UpdatedDate = createdDate;

                stockMovements.Add(new StockMovement
                {
                    ProductId = stock.ProductId,
                    WarehouseId = stock.WarehouseId,
                    MovementType = StockMovementType.Out,
                    Quantity = quantity,
                    ReferenceNumber = order.OrderNumber,
                    Description = "Fake order stock out",
                    CreatedDate = createdDate
                });

                totalAmount += totalPrice;
            }

            if (!order.Items.Any())
                continue;

            order.TotalAmount = totalAmount;
            orders.Add(order);

            payments.Add(new Payment
            {
                Order = order,
                Amount = totalAmount,
                Method = PickPaymentMethod(),
                Status = PickPaymentStatus(order.Status),
                PaidDate = createdDate.AddMinutes(Random.Shared.Next(1, 60)),
                CreatedDate = createdDate
            });

            notifications.Add(new Notification
            {
                UserId = user.Id,
                Title = "Order created",
                Message = $"{order.OrderNumber} order created.",
                IsRead = Random.Shared.NextDouble() < 0.65,
                ReadDate = Random.Shared.NextDouble() < 0.65 ? createdDate.AddHours(1) : null,
                CreatedDate = createdDate
            });
        }

        await _context.Orders.AddRangeAsync(orders, cancellationToken);
        await _context.Payments.AddRangeAsync(payments, cancellationToken);
        await _context.StockMovements.AddRangeAsync(stockMovements, cancellationToken);
        await _context.Notifications.AddRangeAsync(notifications, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }

    private static DateTime GenerateRealisticOrderDate()
    {
        var baseDate = DateTime.UtcNow.AddDays(-Random.Shared.Next(0, 365));

        var isWeekend = baseDate.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;
        var hour = isWeekend
            ? Random.Shared.Next(9, 23)
            : WeightedBusinessHour();

        return new DateTime(
            baseDate.Year,
            baseDate.Month,
            baseDate.Day,
            hour,
            Random.Shared.Next(0, 60),
            Random.Shared.Next(0, 60),
            DateTimeKind.Utc);
    }

    private static int WeightedBusinessHour()
    {
        var roll = Random.Shared.Next(1, 101);

        return roll switch
        {
            <= 5 => Random.Shared.Next(0, 7),
            <= 20 => Random.Shared.Next(7, 10),
            <= 80 => Random.Shared.Next(10, 18),
            _ => Random.Shared.Next(18, 23)
        };
    }

    private static OrderStatus PickOrderStatus()
    {
        var roll = Random.Shared.Next(1, 101);

        return roll switch
        {
            <= 8 => OrderStatus.Cancelled,
            <= 25 => OrderStatus.Shipped,
            <= 70 => OrderStatus.Completed,
            _ => OrderStatus.Confirmed
        };
    }

    private static PaymentMethod PickPaymentMethod()
    {
        var roll = Random.Shared.Next(1, 101);

        return roll switch
        {
            <= 45 => PaymentMethod.CreditCard,
            <= 80 => PaymentMethod.BankTransfer,
            _ => PaymentMethod.Cash
        };
    }

    private static PaymentStatus PickPaymentStatus(OrderStatus orderStatus)
    {
        if (orderStatus == OrderStatus.Cancelled)
            return Random.Shared.NextDouble() < 0.4
                ? PaymentStatus.Refunded
                : PaymentStatus.Failed;

        return Random.Shared.NextDouble() < 0.93
            ? PaymentStatus.Paid
            : PaymentStatus.Pending;
    }
}