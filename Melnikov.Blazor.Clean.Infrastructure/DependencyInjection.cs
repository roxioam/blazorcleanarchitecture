using Melnikov.Blazor.Clean.Application.Common.Interfaces.Repositories;
using Melnikov.Blazor.Clean.Application.Common.Interfaces.Services;
using Melnikov.Blazor.Clean.Application.Common.Permissions;
using Melnikov.Blazor.Clean.Domain.Identity;
using Melnikov.Blazor.Clean.Infrastructure.Constants.Identity;
using Melnikov.Blazor.Clean.Infrastructure.Options;
using Melnikov.Blazor.Clean.Infrastructure.Persistence;
using Melnikov.Blazor.Clean.Infrastructure.Persistence.Repositories;
using Melnikov.Blazor.Clean.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Melnikov.Blazor.Clean.Infrastructure;

public static class DependencyInjection
{
    private const string DefaultConnectionStringName = "DefaultConnection";
    private const string OracleMigrationAssemblyName = "Melnikov.Blazor.Clean.Migration.Oracle";

    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSettings(configuration);
        services.AddDatabase(configuration);
        services.AddAuthenticationService(configuration);
        services.AddRepositories();
        services.AddServices();
        services.AddCacheService();

        return services;
    }

    private static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SmtpClientOptions>(configuration.GetSection(SmtpClientOptions.SmtpClient));

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services,
        IConfiguration configuration)
    {
#if DEBUG
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseOracle(configuration.GetConnectionString(DefaultConnectionStringName),
                x =>
                {
                    x.UseOracleSQLCompatibility(OracleSQLCompatibility.DatabaseVersion19);
                    //x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    x.MigrationsAssembly(OracleMigrationAssemblyName);
                }).EnableSensitiveDataLogging().LogTo(Log.Logger.Information));
#else
    services.AddDbContext<ApplicationDbContext>(options =>
            options.UseOracle(configuration.GetConnectionString(DefaultConnectionStringName),
                x =>
                {
                    x.UseOracleSQLCompatibility(OracleSQLCompatibility.DatabaseVersion19);
                    //x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    x.MigrationsAssembly(OracleMigrationAssemblyName);
                }).LogTo(Log.Logger.Information));
#endif

#if DEBUG
        services.AddDbContextFactory<ApplicationDbContext>(options =>
            options.UseOracle(configuration.GetConnectionString(DefaultConnectionStringName),
                x =>
                {
                    x.UseOracleSQLCompatibility(OracleSQLCompatibility.DatabaseVersion19);
                    //x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    x.MigrationsAssembly(OracleMigrationAssemblyName);
                }).EnableSensitiveDataLogging().LogTo(Log.Logger.Information), ServiceLifetime.Scoped);
#else
        services.AddDbContextFactory<ApplicationDbContext>(options =>
            options.UseOracle(configuration.GetConnectionString(DefaultConnectionStringName),
                x =>
                {
                    x.UseOracleSQLCompatibility(OracleSQLCompatibility.DatabaseVersion19);
                    //x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    x.MigrationsAssembly(OracleMigrationAssemblyName);
                }).LogTo(Log.Logger.Information), ServiceLifetime.Scoped);
#endif

        services.AddScoped<ApplicationDbContextInitializer>();

        return services;
    }

    private static IServiceCollection AddAuthenticationService(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddIdentityCore<ApplicationUser>()
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddClaimsPrincipalFactory<ApplicationUserClaimsPrincipalFactory>()
            .AddDefaultTokenProviders();

        services.AddAuthorizationCore(options =>
            {
                foreach (var permission in PermissionBase.GetAllPermissions())
                {
                    options.AddPolicy(permission,
                        policy => policy.RequireClaim(ApplicationClaimTypes.Permission, permission));
                }
            })
            .AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies();

        //services.AddDataProtection().PersistKeysToDbContext<ApplicationDbContext>();

        services.ConfigureApplicationCookie(options => { options.LoginPath = "/account/login"; });

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepositoryFactory<>), typeof(RepositoryFactory<>));
        services.AddScoped(typeof(IRepositoryFactory<,>), typeof(RepositoryFactory<,>));
        services.AddTransient(typeof(IRepository<,>), typeof(Repository<,>));
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddSingleton<IEmailService, EmailService>();

        return services;
    }

    private static IServiceCollection AddCacheService(this IServiceCollection services)
    {
        services.AddMemoryCache();

        return services;
    }
}