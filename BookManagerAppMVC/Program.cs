using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BookManagerAppMVC.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BookManagerAppMVCContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookManagerAppMVCContext") ?? throw new InvalidOperationException("Connection string 'BookManagerAppMVCContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews().AddViewComponentsAsServices();

// 認証用
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Home/Index";
        option.AccessDeniedPath = "/Home/Index";
    });

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
