using eShopSolution.Application.Catalog.Categories;
using eShopSolution.Application.Catalog.Products;
using eShopSolution.Application.System;
using eShopSolution.Application.System.Languages;
using eShopSolution.Application.System.Roles;
using eShopSolution.Application.System.Users;
using eShopSolution.Application.Utilities.Slides;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.ViewModels.System.Users;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
{
    var connection = builder.Configuration.GetConnectionString("eShopSolutionDb");

    // Add services to the container.
    builder.Services.AddIdentity<AppUser, AppRole>()
        .AddEntityFrameworkStores<EShopDbContext>()
        .AddDefaultTokenProviders();

    builder.Services.AddDbContext<EShopDbContext>(options => options.UseSqlServer(connection));
    builder.Services.AddTransient<IStorageService, FileStorageService>();
    builder.Services.AddTransient<IProductService, ProductService>();
    builder.Services.AddTransient<IUserService, UserService>();
    builder.Services.AddTransient<IRoleService, RoleService>();
    builder.Services.AddTransient<ILanguageService, LanguageService>();
    builder.Services.AddTransient<ICategoryService, CategoryService>();
    builder.Services.AddTransient<ISlideService, SlideService>();

    builder.Services.AddControllers();

    builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>());

    // Register the Swagger generator, defining 1 or more Swagger documents
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo() { Title = "Swagger eShopSolution Solution", Version = "v1" });

        // Bearer token authentication
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            }
        });
    });

    string issuer = "http://localhost:7040";
    string signingKey = "eShopSolutionSecretKey";
    byte[] _signingKey = Encoding.UTF8.GetBytes(signingKey);

    builder.Services.AddAuthentication(otps =>
    {
        otps.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        otps.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = issuer,
            ValidateIssuerSigningKey = true,
            RequireExpirationTime = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(_signingKey)
        };
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

app.UseAuthentication();
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
