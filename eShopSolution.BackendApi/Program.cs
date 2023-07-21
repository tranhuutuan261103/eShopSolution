using eShopSolution.Application.Catalog.Products;
using eShopSolution.Application.System;
using eShopSolution.Application.System.Users;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
{
    var connection = builder.Configuration.GetConnectionString("eShopSolutionDb");

    // Add services to the container.
    builder.Services.AddIdentity<AppUser, AppRole>()
        .AddEntityFrameworkStores<EShopDbContext>()
        .AddDefaultTokenProviders();

    builder.Services.AddDbContext<EShopDbContext>(options => options.UseSqlServer(connection));
    builder.Services.AddTransient<IStorageService, FileStorageService>();
    builder.Services.AddTransient<IPublicProductService, PublicProductService>();
    builder.Services.AddTransient<IManageProductService, ManageProductService>();
    builder.Services.AddTransient<IUserService, UserService>();

    // Register the Swagger generator, defining 1 or more Swagger documents
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo() { Title = "Swagger eShopSolution Solution", Version = "v1" });
    });
}

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Connect to SQL Server

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger eShopSolution Solution V1");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
