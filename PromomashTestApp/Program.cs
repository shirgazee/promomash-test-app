using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PromomashTestApp.Controllers.Models;
using PromomashTestApp.Infrastructure;
using PromomashTestApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
services.AddDbContext<AppDbContext>();
       
services.AddScoped<ICountryRepository, CountryRepository>();
services.AddScoped<IProvinceRepository, ProvinceRepository>();

services.AddControllersWithViews();
services.AddValidatorsFromAssemblyContaining<UserRegisterModelValidator>();

services.AddLogging(logging => logging.AddConsole());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await context.Database.MigrateAsync();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
