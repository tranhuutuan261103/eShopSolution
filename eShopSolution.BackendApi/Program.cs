using eShopSolution.Application.Catalog.Products;
using eShopSolution.Data.EF;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
{
    var connection = builder.Configuration.GetConnectionString("eShopSolutionDb");
    builder.Services.AddDbContext<EShopDbContext>(options => options.UseSqlServer(connection));
    builder.Services.AddTransient<IPublicProductService, PublicProductService>();
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
