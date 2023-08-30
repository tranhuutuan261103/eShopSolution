using eShopSolution.ViewModels.Catalog.Categories;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eShopSolution.WebApp.Models
{
	public class ProductCategoryViewModel
	{
		public CategoryViewModel Category { get; set; }
		public PagedResult<ProductViewModel> Products { get; set; }
	}
}
