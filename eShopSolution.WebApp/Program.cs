using eShopSolution.ApiIntegration;
using eShopSolution.ApiIntegration.Services;
using eShopSolution.WebApp.LocalizationResources;
using LazZiya.ExpressLocalization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var cultures = new[]
	{
		new CultureInfo("vi-VN"),
		new CultureInfo("en-US")
	};
builder.Services.AddControllersWithViews()
				.AddExpressLocalization<ExpressLocalizationResource, ViewLocalizationResource>(ops =>
				{
					// When using all the culture providers, the localization process will
					// check all available culture providers in order to detect the request culture.
					// If the request culture is found it will stop checking and do localization accordingly.
					// If the request culture is not found it will check the next provider by order.
					// If no culture is detected the default culture will be used.

					// Checking order for request culture:
					// 1) RouteSegmentCultureProvider
					//      e.g. http://localhost:1234/tr
					// 2) QueryStringCultureProvider
					//      e.g. http://localhost:1234/?culture=tr
					// 3) CookieCultureProvider
					//      Determines the culture information for a request via the value of a cookie.
					// 4) AcceptedLanguageHeaderRequestCultureProvider
					//      Determines the culture information for a request via the value of the Accept-Language header.
					//      See the browsers language settings

					// Uncomment and set to true to use only route culture provider
					ops.UseAllCultureProviders = false;
					ops.ResourcesPath = "LocalizationResources";
					ops.RequestLocalizationOptions = o =>
					{
						o.SupportedCultures = cultures;
						o.SupportedUICultures = cultures;
						o.DefaultRequestCulture = new RequestCulture("vi-VN");
					};
				});

var culture = System.Globalization.CultureInfo.CurrentCulture.Name;
builder.Services.AddHttpClient();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
	options.LoginPath = "/" + culture + "/Account/Login";
	options.AccessDeniedPath = "/User/Forbidden/";
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddTransient<IUserApiClient, UserApiClient>();
builder.Services.AddTransient<IRoleApiClient, RoleApiClient>();
builder.Services.AddTransient<ILanguageApiClient, LanguageApiClient>();
builder.Services.AddTransient<IProductApiClient, ProductApiClient>();
builder.Services.AddTransient<ICategoryApiClient, CategoryApiClient>();
builder.Services.AddTransient<ISlideApiClient, SlideApiClient>();
builder.Services.AddTransient<IOrderApiClient, OrderApiClient>();

builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30);
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

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.UseRequestLocalization();

app.MapControllerRoute(
name: "default",
    pattern: "{culture=vi-VN}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
name: "Product Category En",
	pattern: "{culture}/categories/{id}",
	new
	{
		Controller = "Product",
		Action = "Category"
	});

app.MapControllerRoute(
name: "Product Category Vn",
	pattern: "{culture}/danh-muc/{id}",
	new
	{
		Controller = "Product",
		Action = "Category"
	});

app.MapControllerRoute(
name: "Product Detail En",
	pattern: "{culture}/products/{id}",
	new
	{
		Controller = "Product",
		Action = "Detail"
	});

app.MapControllerRoute(
name: "Product Detail Vn",
	pattern: "{culture}/san-pham/{id}",
	new
	{
		Controller = "Product",
		Action = "Detail"
	});

	
app.Run();
