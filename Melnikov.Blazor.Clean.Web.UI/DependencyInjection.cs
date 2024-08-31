using Melnikov.Blazor.Clean.Domain.Identity;
using Melnikov.Blazor.Clean.Web.UI.Components;
using Melnikov.Blazor.Clean.Web.UI.Constants;
using Melnikov.Blazor.Clean.Web.UI.Extensions;
using Melnikov.Blazor.Clean.Web.UI.Middlewares;
using Melnikov.Blazor.Clean.Web.UI.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;

namespace Melnikov.Blazor.Clean.Web.UI;

public static class DependencyInjection
{
    public static IServiceCollection AddServerUI(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers();

        services.AddRazorComponents()
            .AddInteractiveServerComponents();

        services.AddCascadingAuthenticationState();

        services.AddScoped<IdentityRedirectManager>();
        services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

        services.AddSingleton<IEmailSender<ApplicationUser>, IdentityEmailSender>();

        services.AddControllers();

        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        services.AddHealthChecks();

        services.AddHttpContextAccessor();
        services.AddScoped<PermissionHelper>();

        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders =
                ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        });

        return services;
    }

    public static WebApplication ConfigureServer(this WebApplication app, IConfiguration config)
    {
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler(RouteConstants.ErrorPage, true);
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseRouting();

        app.UseStatusCodePagesWithRedirects("/404");
        app.MapHealthChecks("/health");

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseAntiforgery();
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Files")))
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Files"));

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Files")),
            RequestPath = new PathString("/Files")
        });

        app.UseExceptionHandler();
        app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

        app.MapControllers();

        // Add additional endpoints required by the Identity /Account Razor components.
        app.MapAdditionalIdentityEndpoints();
        app.UseForwardedHeaders();
        app.UseWebSockets(new WebSocketOptions
        {
            // We obviously need this
            KeepAliveInterval = TimeSpan.FromSeconds(30), // Just in case
        });

        return app;
    }
}