using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Utilities.Slides;

namespace eShopSolution.WebApp.Models
{
	public class HomeViewModel
	{
		public List<SlideViewModel> Slides { get; set;}
		public List<ProductViewModel> FeaturedProducts { get; set;}
	}
}
