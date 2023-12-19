using KMGamesCore.Data.DBContext;
using KMGamesCore.Data.Repository;
using KMGamesCore.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using KMGamesCore.Utilities;
using KMGamesCore.Web.Areas.PayPal.Models;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDBContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDBContext>();

builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

builder.Services.AddSingleton<IEmailSender, EmailSender>();

// PayPal Client configuration

builder.Services.AddSingleton(x =>
    new PayPalClient(
        builder.Configuration["PayPal:ClientId"],
        builder.Configuration["PayPal:Secret"],
        builder.Configuration["PayPal:Mode"]
    )
);

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var cultureInfo = new CultureInfo("en-US");
cultureInfo.NumberFormat.CurrencySymbol = "$";
cultureInfo.NumberFormat.NumberDecimalSeparator = ".";

CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
