using eShopSolution.ApiIntegration.Services;
using eShopSolution.Utilities.Constants;
using eShopSolution.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eShopSolution.WebApp.Controllers
{
	public class CartController : Controller
	{
		private readonly IProductApiClient _productApiClient;
		public CartController(IProductApiClient productApiClient)
		{
			_productApiClient = productApiClient;
		}
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult GetListItems()
		{
			var session = HttpContext.Session.GetString(SystemConstants.Cart);
			if (session != null)
			{
				var list = JsonConvert.DeserializeObject<List<CartItemViewModel>>(session);
				return Ok(list);
			}
			return Ok(new List<CartItemViewModel>());
		}

		[HttpPost]
		public async Task<IActionResult> AddToCart(int id,string languageId)
		{
			var product = await _productApiClient.GetById(id, languageId);

			List<CartItemViewModel> currentCart = new List<CartItemViewModel>();
			var session = HttpContext.Session.GetString(SystemConstants.Cart);
			if (session != null)
			{
				currentCart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(session);
			}

			if (currentCart.Count() != 0)
			{
				if (currentCart.Any(x => x.ProductId == id))
				{
					foreach (var item in currentCart)
					{
						if (item.ProductId == id)
						{
							break;
						}
					}
				}
				else
				{
					var cartItem = new CartItemViewModel()
					{
						ProductId = id,
						Description = product.Description,
						Image = product.ThumbnailImage,
						Name = product.Name,
						Quantity = 1,
						Price = product.Price
					};
					currentCart.Add(cartItem);
				}
				HttpContext.Session.SetString(SystemConstants.Cart, JsonConvert.SerializeObject(currentCart));
			}
			else
			{
				var cartItem = new CartItemViewModel()
				{
					ProductId = id,
					Description = product.Description,
					Image = product.ThumbnailImage,
					Name = product.Name,
					Quantity = 1,
					Price = product.Price
				};
				currentCart.Add(cartItem);
				HttpContext.Session.SetString(SystemConstants.Cart, JsonConvert.SerializeObject(currentCart));
			}

			return Ok(currentCart);
		}

		public IActionResult UpdateCart(int id, int quantity)
		{
			var session = HttpContext.Session.GetString(SystemConstants.Cart);
			List<CartItemViewModel>? list = new List<CartItemViewModel>();
			if (session != null)
			{
				list = JsonConvert.DeserializeObject<List<CartItemViewModel>>(session);
				if (list != null)
				{
					foreach (var item in list)
					{
						if (item.ProductId == id)
						{
							item.Quantity = quantity;
							if (item.Quantity == 0)
							{
								list.Remove(item);
							}
							break;
						}
					}
				}
				
				HttpContext.Session.SetString(SystemConstants.Cart, JsonConvert.SerializeObject(list));
			}
			return Ok(list);
		}
	}
}
