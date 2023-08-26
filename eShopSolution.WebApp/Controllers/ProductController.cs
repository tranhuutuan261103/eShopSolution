using eShopSolution.ApiIntegration.Services;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.WebApp.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductApiClient _productApiClient;
		private readonly ICategoryApiClient _categoryApiClient;
		public ProductController(IProductApiClient productApiClient, ICategoryApiClient categoryApiClient)
		{
			_productApiClient = productApiClient;
			_categoryApiClient = categoryApiClient;
		}
		public IActionResult Detail(int id)
		{
			return View();
		}

		public async Task<IActionResult> Category(int id, string culture, int pageIndex = 1)
		{
			var products = await _productApiClient.GetProductsPaging(new GetManageProductPagingRequest()
			{
				CategoryId = id,
				LanguageId = culture,
				PageIndex = pageIndex,
				PageSize = 10
			});
			var category = await _categoryApiClient.GetById(id, culture);
			return View(new ProductCategoryViewModel()
			{
				Products = products.ResultObj,
				Category = category
			});
		}
	}
}
