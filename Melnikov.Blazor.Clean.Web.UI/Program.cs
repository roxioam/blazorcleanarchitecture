using Melnikov.Blazor.Clean.Application;
using Melnikov.Blazor.Clean.Infrastructure;
using Melnikov.Blazor.Clean.Infrastructure.Persistence;
using Serilog;

namespace Melnikov.Blazor.Clean.Web.UI;

public class Program
{
    public static async Task Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateBootstrapLogger();

        try
        {
            Log.Logger.Information("Starting web host...");

            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((context, provider, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration).ReadFrom.Services(provider));

            builder.WebHost.UseStaticWebAssets();

            builder.Services.AddApplication(builder.Configuration)
                .AddInfrastructure(builder.Configuration)
                .AddServerUI(builder.Configuration);

            var app = builder.Build();

            app.ConfigureServer(builder.Configuration);

            if (app.Environment.IsDevelopment())
            {
                // Initialise and seed database
                using (var scope = app.Services.CreateScope())
                {
                    var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
                    await initializer.InitialiseAsync();
                    await initializer.SeedAsync();
                }
            }

            await app.RunAsync();
        }
        catch (Exception e)
        {
            Log.Logger.Fatal(e, "Fatal exception has been thrown during app working.");
            throw;
        }
        finally
        {
            Log.Logger.Information("Web host has been stopped.");
            await Log.CloseAndFlushAsync();
        }
    }
}