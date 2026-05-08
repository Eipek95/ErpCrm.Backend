using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Infrastructure.CurrentUser;
using ErpCrm.Infrastructure.Security;
using ErpCrm.Infrastructure.Seed;
using Microsoft.Extensions.DependencyInjection;


namespace ErpCrm.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IFakeDataSeeder, FakeDataSeeder>();
        return services;
    }
}