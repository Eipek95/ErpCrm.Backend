using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Options;
using ErpCrm.Infrastructure.Caching;
using ErpCrm.Infrastructure.CurrentUser;
using ErpCrm.Infrastructure.Events;
using ErpCrm.Infrastructure.Security;
using ErpCrm.Infrastructure.Seed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ErpCrm.Infrastructure.Caching;

namespace ErpCrm.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(
     this IServiceCollection services,
     IConfiguration configuration)
    {
        services.Configure<FakeDataOptions>(
            configuration.GetSection("FakeData"));

        services.AddHttpContextAccessor();

        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IFakeDataSeeder, FakeDataSeeder>();
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        //services.AddStackExchangeRedisCache(options =>
        //{
        //    options.Configuration =
        //        configuration["Redis:ConnectionString"];
        //});
        services.AddDistributedMemoryCache();
        services.AddScoped<ICacheService, RedisCacheService>();
        services.Configure<FakeDataOptions>(configuration.GetSection("FakeData"));
        return services;
    }
}