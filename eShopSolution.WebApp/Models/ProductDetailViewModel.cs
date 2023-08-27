using eShopSolution.ViewModels.Catalog.Categories;
using eShopSolution.ViewModels.Catalog.ProductImages;
using eShopSolution.ViewModels.Catalog.Products;

namespace eShopSolution.WebApp.Models
{
	public class ProductDetailViewModel
	{
		public ProductViewModel ProductVM { get; set; }
		public List<ProductViewModel> RelatedProducts { get; set; }
		public List<ProductImageViewModel> ProductImageVM { get; set; }
	}
}
