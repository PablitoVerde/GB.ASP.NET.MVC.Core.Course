using Serilog;
using ASP_NET_Core_MVC_Project.Interfaces;
using ASP_NET_Core_MVC_Project.Models;
using ASP_NET_Core_MVC_Project.Controllers;
using ASP_NET_Core_MVC_Project.Services;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();
Log.Information("Starting up");

try
{
    IConfigurationRoot config = new ConfigurationBuilder()
    .AddUserSecrets<EmailSenderCredentials>(true)
    .Build();

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllersWithViews();

    builder.Services.Configure<SmtpCredentials>(
       builder.Configuration.GetSection("SmtpCredentials"));

    builder.Services.AddSingleton<IConfigurationRoot>(config);

    builder.Services.AddHostedService<BackgroundServiceMailing>();

    builder.Services.AddSingleton<IEmailSender, EmailSenderMailKit>();

    builder.Host.UseSerilog((_, conf) =>
    {
        conf
        .WriteTo.Console()
        .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day);
    });

    var app = builder.Build();

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}



