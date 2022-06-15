using Serilog.AspNetCore;

IConfigurationRoot config = new ConfigurationBuilder()
    .AddUserSecrets<ASP_NET_Core_MVC_Project.Models.EmailSenderCredentials>(true)
    .Build();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<ASP_NET_Core_MVC_Project.Models.SmtpCredentials>(
   builder.Configuration.GetSection("SmtpCredentials"));

builder.Services.AddSingleton<IConfigurationRoot>(config);

builder.Services.AddSingleton<ASP_NET_Core_MVC_Project.Interfaces.IEmailSender, ASP_NET_Core_MVC_Project.Models.EmailSenderMailKit>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
