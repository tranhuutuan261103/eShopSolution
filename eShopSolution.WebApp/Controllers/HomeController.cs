using eShopSolution.ApiIntegration;
using eShopSolution.ApiIntegration.Services;
using eShopSolution.WebApp.Models;
using LazZiya.ExpressLocalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;

namespace eShopSolution.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISharedCultureLocalizer _loc;
        private readonly ISlideApiClient _slideApiClient;
        private readonly IProductApiClient _productApiClient;

        public HomeController(ILogger<HomeController> logger, ISharedCultureLocalizer loc, ISlideApiClient slideApiClient, IProductApiClient productApiClient)
        {
            _logger = logger;
            _loc = loc;
            _slideApiClient = slideApiClient;
            _productApiClient = productApiClient;
        }

        public async Task<IActionResult> Index()
        {
            var languageId = CultureInfo.CurrentCulture.Name;
            var slides = await _slideApiClient.GetAll();
            var featuredProducts = await _productApiClient.GetFeaturedProducts(languageId, 5);
            var latestProducts = await _productApiClient.GetLatestProducts(languageId, 6);
            HomeViewModel homeViewModel = new HomeViewModel()
            {
				Slides = slides,
                FeaturedProducts = featuredProducts,
                LatestProducts = latestProducts
			};
            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SetCultureCookie(string cltr, string returnUrl)
        {
            Response.Cookies.Append(
                                CookieRequestCultureProvider.DefaultCookieName,
                                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cltr)),
                                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                                );
            return LocalRedirect(returnUrl);
        }
    }
}