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
		public async Task<IActionResult> Detail(int id, string culture)
		{
			var product = await _productApiClient.GetById(id, culture);
			var productImages = await _productApiClient.GetListImages(id);
			var relatedProducts = await _productApiClient.GetListProductByCategoryId(id, culture);
			return View(new ProductDetailViewModel()
			{
				ProductVM = product,
				ProductImageVM = productImages,
				RelatedProducts = relatedProducts
			});
		}

		public async Task<IActionResult> Category(int id, string culture, int pageIndex = 1)
		{
			var products = await _productApiClient.GetProductsPaging(new GetManageProductPagingRequest()
			{
				CategoryId = id,
				LanguageId = culture,
				PageIndex = pageIndex,
				PageSize = 2
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
